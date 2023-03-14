using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Supabase.Storage;

namespace EnglishChallengesWebApp.Resources.Model
{
    public class SupabaseClient: ComponentBase
    {
        [Inject]
        protected Supabase.Client Supabase { get; set; }

        protected static readonly string _storageName = "question-image-bank";
        protected static readonly string _CDNURL = "https://qhmtgkkbpwqfroujzubx.supabase.co/storage/v1/object/public/" + _storageName + "/";
        protected static List<FileObject> DatabaseFiles { get; set; } = new();
        protected List<IBrowserFile> LoadedFiles = new();
        protected IBrowserFile? LoadedFile { get; set; }

        protected const long allowFileSizeKB = 1500;
        protected long maxFileSize = 1024 * allowFileSizeKB;
        protected int maxAllowedFiles = 0; // 0 means unlimited
        protected bool IsUploading { get; set; } = false;
        protected bool IsLoading { get; set; } = false;

        protected string SearchQuery { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await RetrieveAllFiles();
        }

        protected async Task RetrieveAllFiles()
        {
            DatabaseFiles = await Supabase.Storage.From(_storageName).List() ?? new();
        }

        protected void LoadFilesMulti(InputFileChangeEventArgs e)
        {
            IsLoading = true;
            LoadedFiles.Clear();

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles == 0 ? 10000000 : maxAllowedFiles))
            {
                try
                {
                    LoadedFiles.Add(file);
                }
                catch
                {
                    Console.WriteLine("Failed to load file " + file.Name);
                    continue;
                }
            }

            IsLoading = false;
        }

        protected async Task UploadFileMulti()
        {
            IsUploading = true;
            foreach (IBrowserFile file in LoadedFiles)
            {
                try
                {
                    using var stream = file.OpenReadStream(maxAllowedSize: maxFileSize);
                    byte[] fileBytes = new byte[stream.Length];
                    await stream.ReadAsync(fileBytes.AsMemory(0, (int)stream.Length));
                    await Supabase.Storage.From(_storageName).Upload(fileBytes, file.Name, new Supabase.Storage.FileOptions { CacheControl = "3600", Upsert = false });
                }
                catch
                {
                    Console.WriteLine("There seems to be a problem uploading file " + file.Name);
                    continue;
                }
            }
            LoadedFiles.Clear();
            IsUploading = false;
            await RetrieveAllFiles();
        }

        protected void LoadFile(InputFileChangeEventArgs e)
        {
            IsLoading = true;

            try
            {
                LoadedFile = e.File;
            }
            catch
            {
                Console.WriteLine("Failed to load file");
            }

            IsLoading = false;
        }

        protected async Task UploadFile()
        {
            IsUploading = true;
            if (LoadedFile != null)
            {
                try
                {
                    using var stream = LoadedFile.OpenReadStream(maxAllowedSize: maxFileSize);
                    byte[] fileBytes = new byte[stream.Length];
                    await stream.ReadAsync(fileBytes.AsMemory(0, (int)stream.Length));
                    await Supabase.Storage.From(_storageName).Upload(fileBytes, LoadedFile.Name, new Supabase.Storage.FileOptions { CacheControl = "3600", Upsert = false });
                }
                catch
                {
                    Console.WriteLine("There seems to be a problem uploading file " + LoadedFile.Name);
                }
            }
            LoadedFile = null;
            IsUploading = false;
            await RetrieveAllFiles();
        }

        protected void CancelUpload()
        {
            LoadedFile = null;
            LoadedFiles.Clear();
        }

        protected async Task DeleteFile(string fileName)
        {
            await Supabase.Storage.From(_storageName).Remove(new List<string> { fileName });
            await RetrieveAllFiles();
        }

        protected void UpdateFile()
        {

        }
        protected async Task SearchFile(string searchQuery)
        {
            SearchOptions options = new() { Search = searchQuery };
            DatabaseFiles = await Supabase.Storage.From(_storageName).List("",options) ?? new();
        }
    }
}
