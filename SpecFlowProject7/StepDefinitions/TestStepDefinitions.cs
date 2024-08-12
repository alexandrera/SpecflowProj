using Microsoft.Playwright;
using NUnit.Framework;


[Binding]
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TestStepDef
{
    private readonly IPage page;
    private readonly IBrowser browser;
    private readonly ScenarioContext myscenarioContext;
    public String postalCodeWebPage = "https://worldpostalcode.com/";

    public TestStepDef(ScenarioContext scenarioContext)
    {
        browser = Playwright.CreateAsync().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }).Result;
        page = browser.NewPageAsync().Result;
        myscenarioContext = scenarioContext;
        page.SetViewportSizeAsync(1920, 1080);
    }


    [Given(@"Enter worldpostalcode website")]
    public async Task Enterworldpostalcodewebsite()
    {
        await page.GotoAsync(postalCodeWebPage);
        
    }

    [When(@"I Look for code'(.*)'")]
    public async Task ILookforcode(string code)
    {
        var scenarioName = myscenarioContext.ScenarioInfo.Title;

        if (scenarioName == "Look for stock code")
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Clique para acessar o menu de" }).ClickAsync();
            await page.GetByLabel("Digite aqui o que você procura").FillAsync(code);
            await page.GetByLabel("Clique para buscar o termo").ClickAsync();

        }
        else
        {
            await page.GetByPlaceholder("Address, Country, City or").FillAsync(code);
            await page.GetByRole(AriaRole.Button, new() { Name = "Lookup" }).ClickAsync();
        }
    }

    [Then(@"Postal code does not exist")]
    public async Task Postalcodedoesnotexist()
    {
        await page.GetByText("No matches found").InnerTextAsync();
        
    }

    [Given(@"Go back to worldpostalcode website")]
    public async Task Gobacktoworldpostalcodewebsite()
    {
        await page.GotoAsync(postalCodeWebPage);
        
    }

    [Then(@"Postal code does exist")]
    public async Task Postalcodedoesexist()
    {
        await page.GetByText("Postal code: 01013-001Postal").ClickAsync();
        await Assertions.Expect(page.Locator("#map_canvas")).ToContainTextAsync("Região Metropolitana de São Paulo");

    }

    [Given(@"Go to B3 website")]
    public async Task GivenGoToBWebsite()
    {
        await page.GotoAsync("https://www.b3.com.br/");
    }

    [Then(@"Stock code does exist")]
    public async Task ThenStockCodeDoesExist()
    {

        await page.WaitForSelectorAsync("text=PETROBRAS");

    }



    [AfterScenario]
    public async Task TearDown()
    {
        await browser.CloseAsync();
        
    }

}