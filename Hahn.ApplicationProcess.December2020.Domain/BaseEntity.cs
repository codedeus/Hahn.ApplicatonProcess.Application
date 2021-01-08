using System;

namespace Hahn.ApplicationProcess.December2020.Domain
{

    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}

