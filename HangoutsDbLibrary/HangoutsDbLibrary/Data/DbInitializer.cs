using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HangoutsContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
