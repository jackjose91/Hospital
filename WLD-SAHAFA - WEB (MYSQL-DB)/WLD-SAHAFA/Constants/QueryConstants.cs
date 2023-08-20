using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLD_SAHAFA.Constants
{
    public static class QueryConstants
    {
        public const string GetCounterDetails = "select top 1 P_RECEIPT_NO as ReceiptNo,P_MWT_LOC as Direction,CounterNo as CounterNo from tb_patient_info";
    }
}