To use this sample, modify the code near the top of Program.cs to specify your storage account credentials.
Replace MYSTORAGEACCOUNT with the name of your Windows Azure storage account.
Replace MYSTORAGEKEY with a key for your Windows Azure storage account.
Storage account projects are managed in the Windows Azure portal (http://windows.azure.com).

// Specify your storage project credentials here.
// NOTE: Best practice is not to embed credentials in code or leave in the clear.
const string StorageAccount = "MYSTORAGEACCOUNT";
const string StorageKey = "MYSTORAGEKEY";

Warning: this program creates, modifies, and deletes data. Only use it with a test storage area.
