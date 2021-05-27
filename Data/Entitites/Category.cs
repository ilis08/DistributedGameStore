﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entitites
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
