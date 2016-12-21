namespace Sitecore.Support.Analytics.Data.DataAccess.MongoDb
{
  using MongoDB.Driver;
  using Sitecore.Analytics.DataAccess;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Diagnostics;

  [UsedImplicitly]
  public class MongoDbDataAdapterProvider : Sitecore.Analytics.Data.DataAccess.MongoDb.MongoDbDataAdapterProvider
  {
    public override bool SaveContact(IContact contact, ContactSaveOptions saveOptions)
    {
      try
      {
        return base.SaveContact(contact, saveOptions);
      }
      catch (MongoDuplicateKeyException ex)
      {
        Log.Warn($"{typeof(MongoDuplicateKeyException).Name} ocurred in {typeof(MongoDbDataAdapterProvider).Name}.{nameof(SaveContact)}. Message: {ex.Message}", this);
        Log.Info("Retrying {typeof(MongoDbDataAdapterProvider).Name}.{nameof(SaveContact)}...", this);

        // try to retry                               
        return base.SaveContact(contact, saveOptions);
      }
    }
  }
}