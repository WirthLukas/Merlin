using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Merlin.ECS
{
    public class EntityFilter : IEntityFilter
    {
        private readonly HashSet<string> _mustHaveComponentNames = new();
        private readonly HashSet<string> _mustNotHaveComponentNames = new();

        public IEnumerable<string> MustHaveComponentNames => _mustHaveComponentNames;
        public IEnumerable<string> MustNotHaveComponentNames => _mustNotHaveComponentNames;

        public IEntityFilter MustHave(params string[] names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            foreach (var name in names)
                _mustHaveComponentNames.Add(name);

            return this;
        }

        public IEntityFilter MustNotHave(params string[] names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            foreach (var name in names)
                _mustNotHaveComponentNames.Add(name);

            return this;
        }

        public bool Check(IEntity entity)
        {
            foreach (var name in _mustHaveComponentNames)
            {
                if (!entity.HasComponent (name)) return false;
            }

            foreach (var name in _mustNotHaveComponentNames)
            {
                if (entity.HasComponent (name)) return false;
            }

            return true;
        }
    }
}
