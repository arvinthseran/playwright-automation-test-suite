using FrameworkHelpers;
using TechTalk.SpecFlow;

namespace ConfigurationBuilder;

public class TestDataAttachment(ScenarioContext context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    public void AddTestDataAttachment() => TestAttachmentHelper.AddTestDataAttachment(_objectContext.GetDirectory(), _objectContext.GetAll());
}