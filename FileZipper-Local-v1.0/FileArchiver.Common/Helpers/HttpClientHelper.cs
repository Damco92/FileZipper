using FileArchiverCommon.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FileArchiver.Services.Helpers
{
    public static class HttpClientHelper
    {
        static UserViewModel user;

        public static async Task<List<UserViewModel>> GetAllUsers(string baseAddress, string method)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(method);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Could not load users");
                }

                users = JsonConvert.DeserializeObject<List<UserViewModel>>(await response.Content.ReadAsStringAsync());
            }
            return users;
        }

        public static async Task<UserViewModel> GetUser(CredentialsViewModel credentials, string urlFromConfig)
        {
            var json = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = urlFromConfig;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("User not found");
                }
                user = JsonConvert.DeserializeObject<UserViewModel>(await response.Content.ReadAsStringAsync());
            }
            return user;
        }

        public static async Task<UserViewModel> GetUserByUsername(string username, string baseAddress, string method)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(method + username);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("User not found");
                }
                user = JsonConvert.DeserializeObject<UserViewModel>(await response.Content.ReadAsStringAsync());
            }
            return user;
        }

        public static async Task<string> UpdateUser(UpdateUsersViewModel updateUsersViewModel, string urlFromConfig)
        {
            var json = JsonConvert.SerializeObject(updateUsersViewModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = urlFromConfig;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("User not updated");
                }
                return await response.Content.ReadAsStringAsync();
            }
        }
        public static async Task<FileViewModel> GetFile(int fileId, string baseAddress, string method)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(method + fileId);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("User not found");
                }
                var myMagicFileFinaly = JsonConvert.DeserializeObject<FileViewModel>(await response.Content.ReadAsStringAsync());

                return myMagicFileFinaly;
            }
        }
        public static async Task<List<DocumentTypeViewModel>> GetAllDocumentTypes(string baseAddress, string method)
        {
            List<DocumentTypeViewModel> result = new List<DocumentTypeViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(method);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Could not load document types");
                }

                result = JsonConvert.DeserializeObject<List<DocumentTypeViewModel>>(await response.Content.ReadAsStringAsync());
            }
            return result;
        }

        public static async Task<string> GetMaskByDocumentName(string documentName)
        {
            string result;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58506");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"/api/documentTypes/getMaskForDocument/{documentName}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Could not load document types");
                }
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public static async Task<DocumentTypeViewModel> GetDocumentTypeByDocumentName(string documentName)
        {
            DocumentTypeViewModel result;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58506");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"/api/documentTypes/getDocumentTypeByDocumentName/{documentName}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Could not load document types");
                }
                result =  JsonConvert.DeserializeObject<DocumentTypeViewModel>(await response.Content.ReadAsStringAsync());
            }
            return result;
        }

        public static async Task<string> UploadFile(FileViewModel fileViewModel, string urlFromConfig)
        {
            string result;
            var url = urlFromConfig;

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(fileViewModel.FileId.ToString()), "FileId");
            multipartContent.Add(new StringContent(fileViewModel.FileData.FileName), "FileName");
            multipartContent.Add(new StringContent(fileViewModel.DocumentName), "DocumentName");
            multipartContent.Add(new StringContent(fileViewModel.CreatorUsername), "CreatorUsername");
            multipartContent.Add(new StringContent(fileViewModel.UserThatAFileIsUploadedToUsername), "UserThatAFileIsUploadedToUsername");
            multipartContent.Add(new StreamContent(fileViewModel.FileData.OpenReadStream()), "FileData", fileViewModel.FileData.FileName);

            using (HttpClient client = new HttpClient())
            {
                
                var response = await client.PostAsync(url, multipartContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("File not uploaded");
                }
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public static async Task<string> GetUsersPasswordByUsername(string username)
        {
            string result;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58506");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"/api/users/GetUsersEncryptedPassword/{username}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Password not found");
                }
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}
