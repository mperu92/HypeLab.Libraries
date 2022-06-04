# HypeLab.RxPatternsResolver
Provides a class capable of solve collections of regex patterns given an input string. Also equipped with a default patterns set.

```c#
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
