namespace EventBus.Message.Event;

public class IntegrationBaseEvent
{
  private Guid Id { get; set; }
  private DateTime CreationDate { get; set; }
  
  public IntegrationBaseEvent()
  {
    Id = Guid.NewGuid();
    CreationDate = DateTime.UtcNow;
  }
  
  public IntegrationBaseEvent(Guid id, DateTime creationDate)
  {
    Id = id;
    CreationDate = creationDate;
  }
}