using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glocation.Dominio.Entidades
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class BaseEntity
    {
        #region Members

        /// <summary>
        /// requested hash code
        /// </summary>
        int? _requestedHashCode;
        /// <summary>
        /// identificador
        /// </summary>
        int _id;
        #endregion

        #region Properties

        /// <summary>
        /// Get persisten object identificador
        /// </summary>
        /// <value>identificador.</value>
        [NotMapped]
        public virtual int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public bool IsTransient()
        {
            return Id == default(int);
        }

        /// <summary>
        /// Change current identity for a new non transient identity
        /// </summary>
        /// <param name="identity">new identity</param>
        public void ChangeCurrentIdentity(int identity)
        {
            if (identity != default(int))
                Id = identity;
        }

        #endregion

        #region Overrides Methods

        /// <summary>
        /// <see cref="M:System.Object.Equals" />
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals" /></param>
        /// <returns><see cref="M:System.Object.Equals" /></returns>
        public override bool Equals(object obj)
        {
// ReSharper disable once RedundantComparisonWithNull
            if (obj == null || !(obj is BaseEntity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            BaseEntity item = (BaseEntity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            return item.Id == Id;
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode" />
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode" /></returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
// ReSharper disable once NonReadonlyFieldInGetHashCode
                if (!_requestedHashCode.HasValue)
// ReSharper disable NonReadonlyFieldInGetHashCode
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
// ReSharper restore NonReadonlyFieldInGetHashCode

// ReSharper disable NonReadonlyFieldInGetHashCode
                return _requestedHashCode.Value;
// ReSharper restore NonReadonlyFieldInGetHashCode
            }
// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
// ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
        }

        /// <summary>
        /// Implements ==.
        /// </summary>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>result of operator.</returns>
        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        /// <summary>
        /// Implements !=.
        /// </summary>
        /// <param name="left">left.</param>
        /// <param name="right">right.</param>
        /// <returns>result of operator.</returns>
        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        #endregion
    }
}
