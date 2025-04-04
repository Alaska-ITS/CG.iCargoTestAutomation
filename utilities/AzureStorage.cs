using Azure.Storage.Blobs;
using System;
using System.IO;

namespace iCargoUIAutomation.utilities
{
    public class AzureStorage
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly KeyVault keyVault;        

        public AzureStorage(string containerName)
        {
            keyVault = new KeyVault();
            var secrets = keyVault.GetSecrets();
            _connectionString = secrets["AzureStorageConnectionString"];
            _containerName = containerName;
        }

        public string DownloadFileFromBlob(string fileName, string localFilePath)
        {
            // Create a BlobServiceClient
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Get a reference to a container
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            try
            {
                // Download the blob's contents to a file
                Console.WriteLine($"Attempting to download blob: {fileName}");
                blobClient.DownloadTo(localFilePath);
                Console.WriteLine($"Download successful. File saved to: {localFilePath}");
            }
            catch (Azure.RequestFailedException ex)
            {
                if (ex.Status == 404)
                {
                    // Blob does not exist; return a path indicating a new file will be created
                    Console.WriteLine("Blob does not exist. A new file will be created.");
                    return localFilePath; // Return the provided local file path
                }
                else
                {
                    Console.WriteLine($"Download failed with error: {ex.Message}");
                    throw;
                }
            }

            return localFilePath;
        }

        public void UploadFileToBlob(string localFilePath, string fileName)
        {
            try
            {
                // Create a BlobServiceClient
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

                // Get a reference to a container
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

                // Create the container if it doesn't exist
                containerClient.CreateIfNotExists();

                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                // Upload the file
                Console.WriteLine($"Uploading file to blob: {fileName}");
                blobClient.Upload(localFilePath, overwrite: true);
                Console.WriteLine("Upload successful.");

                //print the path of the blob file uploaded
                Console.WriteLine($"Blob file path: {blobClient.Uri}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload failed with error: {ex.Message}");
                throw;
            }
        }

        public void UploadFolderToAzure(string folderPath)
        {
            try
            {
                // Create a BlobServiceClient
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

                // Get a reference to a container
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

                // Create the container if it doesn't exist
                containerClient.CreateIfNotExists();

                // Get all files in the folder
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    // Get the relative path of the file to maintain the folder structure
                    string relativePath = Path.GetRelativePath(folderPath, file);
                    string blobName = Path.Combine(Path.GetFileName(folderPath), relativePath).Replace("\\", "/");

                    // Get a reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);

                    // Upload the file
                    Console.WriteLine($"Uploading file to blob: {blobName}");
                    blobClient.Upload(file, overwrite: true);
                    Console.WriteLine("File upload successful.");

                    //Hooks.Hooks.uploadedBlobPaths.Add(blobClient.Uri.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Folder upload failed with error: {ex.Message}");
                throw;
            }
        }

        public List<string> GetBlobFileNames()
        {
            var blobClient = new BlobServiceClient(_connectionString);
            var containerClient = blobClient.GetBlobContainerClient(_containerName);

            var fileNames = new List<string>();
            foreach (var blobItem in containerClient.GetBlobs())
            {
                fileNames.Add(blobItem.Name);
            }
            return fileNames;
        }


    }
}
