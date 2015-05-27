﻿using System.Collections.Generic;

namespace DynaApp.Entities
{
    class Domain
    {
        public Domain(params int[] theRange)
        {
            this.Values = new List<int>(theRange);
        }

        public Domain(string theDomainExpression)
        {
            this.Values = new List<int>();
        }

        /// <summary>
        /// Gets the domain values.
        /// </summary>
        public IEnumerable<int> Values { get; private set; }

        /// <summary>
        /// Gets or sets the model the constraint is a part of.
        /// </summary>
        public Model Model { get; set; }

        public static Domain CreateFrom(params int[] theRange)
        {
            return new Domain(theRange);
        }
    }
}
