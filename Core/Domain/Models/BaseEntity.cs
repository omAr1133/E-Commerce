namespace Domain.Models
{
    //parent for all Entities {Domain Models}=> {SQL Server DB} =>Store DB Context
    public class BaseEntity<TKey>
    { 
        public TKey Id { get; set; }
    }

}
