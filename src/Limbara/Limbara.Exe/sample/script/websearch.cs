Host.Log("keep in mind that parts of the script fail when chrome devtools are attached to the browser.");
Host.Log("this sample depends on the path to chrome.exe and a free address already configured in the UI (\"Interface\"->\"config\" tab)");
Host.Log("if you have questions about the API or syntax or need help with a script, visit the forum at http://forum.botengine.de/cat/web/ or take a look at the wiki at https://github.com/Arcitectus/Limbara/wiki/");
Host.Log("\nthis script navigates to a web search engine and fills the form to initiate a search.");

var app = Limbara.App;

if(null == app)
	throw new ArgumentNullException("App object not available. Make sure you are using an up to date version of the application.", "app");

var process = app.ReuseOrCreateProcess()?.Result;

var	connectionAttempt	= process?.ReuseOrOpenConnection();

var webBrowser = connectionAttempt?.Result;

if(null == webBrowser)
	throw new Exception("failed to connect to browser, " + connectionAttempt?.Error?.Message);

Host.Log("connected to browser.");
Host.Delay(1111);
Host.Log("navigating to website.");

webBrowser.Document.Result.locationHref = "http://duckduckgo.com";

Host.Log("waiting for website to load.");
Host.Delay(4444);

var searchBox = webBrowser.Document?.Result?.GetElementFromXPath("//input[@type='text']")?.Result;

if(null == searchBox)
	throw new Exception("search box not found");

Host.Log("search box found, entering search term.");
searchBox.setAttribute("value", "Limbara");

var submitButton = webBrowser.Document?.Result?.GetElementFromXPath("//input[@type='submit']")?.Result;

if(null == submitButton)
	throw new Exception("submit button not found");

Host.Log("submit button found, clicking.");
submitButton.click();
