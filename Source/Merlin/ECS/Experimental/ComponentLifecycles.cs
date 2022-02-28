using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.ECS.Experimental;

public interface IOnAddedToEntity
{
    void OnAddedToEntity<TEntity>(in TEntity entity);
}

public interface IOnRemovedFromEntity
{
    void OnRemovedFromEntity<TEntity>(in TEntity entity);
}
