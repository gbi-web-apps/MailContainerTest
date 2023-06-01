namespace MailContainerTest.Abstractions;

public interface IUnitOfWork
{
    void Commit();
    
    void Rollback();
}