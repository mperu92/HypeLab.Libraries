# HypeLab.RxPatternsResolver
Provides a class capable of solve collections of regex patterns given an input string. Also equipped with a default patterns set.

```c#
using HypeLab.RxPatternsResolver;
using HypeLab.RxPatternsResolver.Constants;

// ...

const string tst = @"Hi i do tes#TS s@ds a\a  b/b°?mlkm";

RegexPatternsResolver resolver = new();
resolver.AddPattern(RxResolverConst.DefaultBadCharsCollectionPattern1, string.Empty);
resolver.AddPattern(@"[/\\]", " - ");
string output = resolver.ResolveStringWithPatterns(tst);

Console.WriteLine($"Old string:{Environment.NewLine}{tst}" +
    Environment.NewLine + Environment.NewLine +
    $"New string:{Environment.NewLine}{output}");
	
	
// Old string:
// Hi i do tes#TS s@ds a\a  b/b°?mlkm

// New string:
// Hi i do tesTS sds a - a  b - b?mlkm
```

## (optional) Register type

based on the version of the framework in use:
```c#
// based on the version of the framework in use:
builder.Services.AddRegexResolver();
// or
services.AddRegexResolver();
```

**Using with DI**
```c#

public class Class1
{
	private readonly RegexPatternsResolver _rxResolver;
	
	public Class1(RegexPatternsResolver rxResolver)
	{
		_rxResolver = rxResolver;
	}
}
```
