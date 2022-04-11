### Mail Container Test 

The code for this exercise has been developed to manage the transfer of mail items from one container to another for processing.

#### Process for transferring mail

- Lookup the container the mail is being transferred from.
- Check the containers are in a valid state for the transfer to take place.
- Reduce the container capacity on the source container and increase the destination container capacity by the same amount.

#### Restrictions

- A container can only hold one type of mail.


#### Assumptions

- For the sake of simplicity, we can assume the containers have an unlimited capacity.

### The exercise brief

The exercise is to take the code in the solution and refactor it into a more suitable approach with the following things in mind:

- Testability
- Readability
- SOLID principles
- Architectural design of the code

You should not change the method signature of the MakeMailTransfer method.

You should add suitable tests into the MailContainerTest.Test project.

There are no additional constraints, use the packages and approach you feel appropriate, aim to spend no more than 2 hours. Please update the readme with specific comments on any areas that are unfinished and what you would cover given more time.

### Notes from Paul Dennis.  9/4/2022.

Spent about 1.5h on prep before starting to code test but I did generate a few files en route as it was convenient. 

Observations on first reading.

Quick look at project shows need of proper stack - business object, services and implementation layers. not worth doing this for the exercise.
DRY - possible merge of MailType and AllowedMailType
DIP - extract IMailContainerDataStore for *DataStore to implement
SRP - acquiring data store type is not responsibility of mail store service, which reveals:
DIP - have MailContainerDataStore depend on IMailContainerDataStore and inject it - SimpleInjector or another component can then determine the data store implementation
Smell - AllowedMailType not specified with [Flags] attribute, auto numbered enumerations will not result in bit masks and there is no indication other than the code to suggest that's what is needed.
DRY / Style / Smell - switch can be replaced with a few lines of much clearer code.
YAGNI - Capacity is not used due to assumptions.
[Flags] option is a red herring due to restriction of single mail type per container.

Extrapolation of acceptance criteria, test requirements & base data

1. "- Lookup the container the mail is being transferred from."

- test data store with both backup and live implementations
- store created mail containers of every mail type in test class level collection
- need test to look up good and bad container IDs 

2. "- Reduce the container capacity on the source container and increase the destination container capacity by the same amount."

Capacity is a badly named member.  It should be "Count" and hence MailContainer could easily inherit from List<MailItem> but that would necessitate the creation of mail items whereas the exercise can be completed by just modifying a count of items 

- test for failure result when requested quantity exceeds container contents
- test for validated containers counts being updated correctly

3. "- Check the containers are in a valid state for the transfer to take place."

There is no provision for setting the container status.  this is the data store's responsibility so implement SetContainerStatus thru IMailContainerDataStore.

- test for good status continues to next validation step
- test for bad container status return failure result.


TODO:
- install a test framework, adaptor, test sdk and a DI framework and run a dummy test
- write initial tests
-- lookup source container (won't work, code not there)
-- check "capacity" (member needs renaming)
-- check mail type (enums need merging, and ditch HasFlag)
-- check container state
-- check "capacity" reduced after transfer
- create abstract base class for data store
- move code to base as virtual methods 
- create interface for data store and have service depend on that via constructor injection
- create an interface for checking config settings
- create implementation of IConfigService to use ConfigurationManager and move AppSettings[] code to that 
- have the test determine which data store to pass to the transfer service
- rename capacity to MailCount
- set member values via constructor
- ditto container creation
- get rid of switch in MakeMailTransfer and replace with Chain Of Responsibility pattern to perform validations on the request object.  that's the scalable solution.
for now, a simple while will suffice.
- replace Success bool with enum Status so we know *why* it failed.
 




