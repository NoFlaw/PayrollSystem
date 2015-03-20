namespace PayrollSystemDemo.Service.Helpers
{
    public static class DiscountHelper
    {
        /// <summary>
        /// Checks first letter in in parameters passed to it
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static int GetDiscountByName(string firstName, string lastName)
        {
            if (firstName.ToUpper()[0] == 'A' || lastName.ToUpper()[0] == 'A')
                return 2;
            return 1;
        }
    }
}