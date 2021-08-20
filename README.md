# TestRunnerAPI
An experiment to try and put a REST API in front of dotnet test

Nothing super special going on really. While working on a project that uses Specflow we came across a challenge whereby there aren't really any easy ways outside of Visual Studio's test runner to put a UI in front of the test scenarios for people to enumerate, select and execute tests. You can use **dotnet test** but it's command-line and not easy to use for non-technical people and you need direct access to the machine the test framework is running on.

This repo is an experiment in making a REST API that can kick off tests via dotnet script and where you can query the status of the execution. Watch this space.
