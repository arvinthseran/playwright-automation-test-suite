[![Build Status](https://seraninfotech.visualstudio.com/BubbaSolution/_apis/build/status/arvinthseran.tfl-enterprise-automation-test-suite?branchName=journeyplanner_tests)](https://seraninfotech.visualstudio.com/BubbaSolution/_build/latest?definitionId=2&branchName=journeyplanner_tests)

# playwright-automation-suite

This is a SpecFlow-Plawright functional UI and mobile web testing framework created using Playwright with NUnit and C# (.Net core) in SpecFlow BDD methodology and Page Object Pattern.

## Prerequisites to run the application:
1. Visual Studio (2022 with V17.4 or higher)
2. Playwright

## Framework:

```ConfigurationBuilder``` project is responsible for constructing configs from multiple json files.
 - User secrets are used for sensitive test data

```Framework``` project is responsible for supporting test execution.
 - Support for playwright deriver
 - Support for both local (dev machine) and cloud (Azure devops) execution
 
## SpecFlow Tests:

```Pret.UITests:``` 
 - Acceptance tests are written in feature files under ```/Tests/Features/``` folder using standard Gherkin language using Given, When, Then, But format with an associated step definition for each test step. Test steps in the scenarios explains the business conditions/behaviour and the associated step definition defines how the scenario steps are automated.
 - Support for parallel execution

## Build and Release process:

### Build 
Every commit made (merge to master or a push to remote branch) will trigger a build process under `arvinthseran.enterprise-automation-test-suite` pipeline.

### Release 
Every release pipeline would be picking up the build artifact from `arvinthseran.playwright-automation-test-suite` build

## Parallel Test Execution Limitations:

This framework supports Feature Level parallelization (tests under different feature file will run in parallel) not Scenario Level parallelization (tests under same feature file will not execute in parallel).
