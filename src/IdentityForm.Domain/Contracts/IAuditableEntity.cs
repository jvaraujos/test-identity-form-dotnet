using System;

namespace IdentityForm.Domain.Contracts
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedOn { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }
    }

    public interface INonAuditableEntity<TId> : INonAuditableEntity, IEntity<TId>
    {
    }

    public interface INonAuditableEntity : IEntity
    {
    }
}