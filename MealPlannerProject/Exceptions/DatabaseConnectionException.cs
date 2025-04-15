namespace MealPlannerProject.Exceptions
{
    using System;

    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException()
        {
        }

        public DatabaseConnectionException(string message)
            : base(message)
        {
        }

        public DatabaseConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
