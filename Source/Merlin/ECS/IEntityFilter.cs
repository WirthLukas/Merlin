using System.Collections.Generic;

namespace Merlin.ECS
{
    // https://github.com/adizhavo/ECS/blob/master/ECS/Filter.cs
    public interface IEntityFilter
    {
        IEnumerable<string> MustHaveComponentNames { get; }
        IEnumerable<string> MustNotHaveComponentNames { get; }

        IEntityFilter MustHave(params string[] types);
        IEntityFilter MustNotHave(params string[] types);
        bool Check(IEntity entity);
    }
}
