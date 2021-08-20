using System;

namespace CanedoLab.MS.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) 
            {
                return true;
            }

            if (ReferenceEquals(null, compareTo)) 
            {
                return false;
            }

            return Id.Equals(compareTo.Id);
        }

        public static bool operator !=(Entity left, Entity right) 
        {
            return left != right;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left == right;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 537 + Id.GetHashCode();
        }
    }
}
