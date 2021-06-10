# Managed ID test

local.settings.json に `AZURE_CLIENT_ID` と `AZURE_TENANT_ID` と `AZURE_CLIENT_SECRET` を追加して実行

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "BLOB_URL": "https://<your storage account name></your>.blob.core.windows.net/",
    "AZURE_CLIENT_ID": "<your app id>",
    "AZURE_TENANT_ID": "<your tenant id>",
    "AZURE_CLIENT_SECRET": "<your app secret>"
  }
}
```

