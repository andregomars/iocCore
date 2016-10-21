using System;

namespace iocCoreSMS.Services
{
    public static class Common
    {
        public static string[] SplitReceiverCodes(string receiverCodes)
        {
            if (String.IsNullOrEmpty(receiverCodes)) return null;

            string[] addrArray = null;
            try
            {
                addrArray = receiverCodes.Split(',');
            }
            catch(Exception e)
            {
                throw new Exception(@"Multiple receiver codes should be separated by comma, 
                    e.g ""tel:+16261112222,tel:+19092223333"", but the input is: " +
                    receiverCodes, e);
            }

            return addrArray;
        }
    }
}