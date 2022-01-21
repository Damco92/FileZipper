﻿using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using FileArchiver.Services.Services.Interfaces;
using FileArchiverCommmon.Helpers;
using FileArchiverCommon.ViewModels;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace FileArchiver.Services.Services.Implementation
{
    public class FilesService : IFilesService
    {
        private IFilesRepository _filesRepository;
        private IUsersRepository _userRepository;
        public FilesService(IFilesRepository filesRepository, IUsersRepository usersRepository)
        {
            _filesRepository = filesRepository;
            _userRepository = usersRepository;
        }

        public IEnumerable<FileViewModel> GetAllFiles()
        {
            var files = _filesRepository.GetAllFiles();
            return files.Select(f => new FileViewModel(f.Id, f.IsDownloaded ,f.FileName,f.Created,f.UserId, f.DocumentTypeId, f.Confirmed));
        }

        //public FileViewModel GetFileByFileId(int fileId) // ??
        //{
        //    FileViewModel result = new FileViewModel();
        //    var file = _filesRepository.GetFileById(fileId);
        //    result.FileId = file.Id;
        //    return result;
        //}

        public FileViewModel GetFileById(int fileId, string domainPassword)
        {
            FileViewModel result = new FileViewModel();
            var file = _filesRepository.GetFileById(fileId);
            Users user = _userRepository.GetUserById(file.UserId);//UserId
            if (file == null)
                return result;

            result.FileId = file.Id;
            result.FileName = file.FileName;
            result.IsDownloaded = file.IsDownloaded;
            result.Created = file.Created;
            result.IsConfirmed = file.Confirmed;
            var decompressedFileData = Decompress(file.Data);
            #region Ziping
            try
            {
                var encryptedPass = user.ZipPassword;
                byte[] bytesToBedDecrypted = Convert.FromBase64String(encryptedPass);
                var keyByteArray = HashingHelper.GetDomainPasswordByteArray(domainPassword);
                var decryptedPasswordBytes = HashingHelper.AES_Decrypt(bytesToBedDecrypted, keyByteArray);
                var decryptedResult = Encoding.UTF8.GetString(decryptedPasswordBytes);
                using (var ms = new MemoryStream())
                {
                    using (var outStream = new ZipOutputStream(ms))
                    {

                        outStream.SetLevel(9);
                        outStream.Password = decryptedResult;
                        outStream.PutNextEntry(new ZipEntry(file.FileName));
                        outStream.Write(decompressedFileData);
                    }
                    result.FileByteData = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "You can not download an other user file";
            }
            #endregion
            return result;
        }
        public FileViewModel GetFileByFileNameAndUsername(string username, string fileName)
        {
            FileViewModel result = new FileViewModel();
            var file = _filesRepository.GetFileByFileNameAndUsername(username, fileName);
            if (file == null)
                return result;
            result.FileId = file.Id;
            result.FileName = file.FileName;
            result.IsDownloaded = file.IsDownloaded;
            result.Created = file.Created;
            result.FileDataByteArray[0].Data = file.Data;
            result.UserVM = MapUserToUserVM(file.User);
            result.DocumentType = MapDocumentTypeToDocumentTypeViewModel(file.DocumentType);
            return result;
        }

        public List<FileViewModel> GetAllFilesByUsername(string username)
        {
            return _filesRepository.GetAllFilesByUsername(username).Select(x => new FileViewModel(x.Id, x.IsDownloaded, x.FileName, x.Created, x.UserId, x.DocumentTypeId, x.Confirmed)).ToList();
        }
        public List<FileViewModel> GetAllFilesByUserId(int Id)
        {
            return _filesRepository.GetAllFilesByUserId(Id).Select(x => new FileViewModel(x.Id, x.IsDownloaded, x.FileName, x.Created, x.UserId, x.DocumentTypeId, x.Confirmed)).ToList();
        }

        public void UploadFile(FileViewModel fileVM)
        {
            byte[] bytes = new byte[4064];
            Files file = new Files();
            file.Created = DateTime.Now;
            file.Creator = fileVM.CreatorId; 
            file.IsDownloaded = fileVM.IsDownloaded;
            file.UserId = fileVM.UserId;
            file.DocumentTypeId = fileVM.DocumentType.Id;
            file.FileName = fileVM.FileName;
            if (fileVM.FileData.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileVM.FileData.CopyTo(ms);
                    bytes = ms.ToArray();
                    ms.Close();
                }
            }
            file.Data = Compress(bytes);
            _filesRepository.InsertFile(file);
        }

        private UserViewModel MapUserToUserVM(Users user)
        {
            UserViewModel result = new UserViewModel(user.Id, user.Username, user.Name, user.IsAdmin);

            result.Files = user.Files.Select(x => new FileViewModel(x.Id, x.IsDownloaded, x.FileName, x.Created, x.UserId, x.DocumentTypeId, x.Confirmed)).ToList();
            return result;
        }

        private DocumentTypeViewModel MapDocumentTypeToDocumentTypeViewModel(DocumentTypes documentType)
        {
            return new DocumentTypeViewModel(documentType.Id, documentType.DocumentName, documentType.FileNameMask, documentType.IsActive);
        }
        private UploadFileViewModel MapFilesToUploadFileViewModel(Files files)
        {
            return new UploadFileViewModel(files.User.Username, files.DocumentTypeId,files.FileName);
        }

        private byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Close();
                    return compressedStream.ToArray();
                }
            }
        }

        byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        public void UpdateFileToDownloaded(int fileId)
        {
            var file = _filesRepository.GetFileById(fileId);
            file.IsDownloaded = true;
            _filesRepository.UpdateFile(file);
        }

        public void UpdateStatusToConfirmed(int fileId)
        {
            var file = _filesRepository.GetFileById(fileId);
            file.Confirmed = true;
            _filesRepository.UpdateFile(file);
        }

        public void UploadMultipleFiles(List<FileViewModel> filesVM)
        {
            filesVM.ForEach(fvm => UploadFile(fvm));
        }
    }
}
