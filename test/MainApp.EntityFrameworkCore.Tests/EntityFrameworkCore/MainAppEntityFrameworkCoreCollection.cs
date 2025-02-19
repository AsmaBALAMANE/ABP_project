using Xunit;

namespace MainApp.EntityFrameworkCore;

[CollectionDefinition(MainAppTestConsts.CollectionDefinitionName)]
public class MainAppEntityFrameworkCoreCollection : ICollectionFixture<MainAppEntityFrameworkCoreFixture>
{

}
