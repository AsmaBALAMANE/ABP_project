﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MainApp.Pages;

[Collection(MainAppTestConsts.CollectionDefinitionName)]
public class Index_Tests : MainAppWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
