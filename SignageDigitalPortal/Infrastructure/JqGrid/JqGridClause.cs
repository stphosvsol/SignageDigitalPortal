using System;
using Infrastructure.JqGrid.Model;
using Infrastructure.Resources;

namespace Infrastructure.JqGrid
{
    public class JqGridClause
    {
        public const string JQGRID_DESC = "desc";
        public const string SQL_DESC = "descending";
        public const string JQGRID_ASC = "asc";
        public const string SQL_ASC = "ascending";

        public const string SQL_OPERATOR_AND = "and";
        public const string SQL_OPERATOR_BW = "bw";
        public const string SQL_OPERATOR_BW_SW = ".StartsWith(@{0})";


        public static string OrderBy(JqGridFilterModel opt)
        {
            return String.Format("{0} {1}", opt.sidx, opt.sord.Trim().ToLower() == JQGRID_DESC ? SQL_DESC : SQL_ASC);
        }

        public static JqGridQueryWhere ExpressionToExec(JqGridMultipleFilterModel mulFilter)
        {
            var count = 0;
            var sQuery = String.Empty;
            var respQry = new JqGridQueryWhere();

            foreach (var rule in mulFilter.rules)
            {
                var qRule = String.Format("{0}{1}", SafeName(rule.field), ConcatOp(rule.op, count));
                respQry.LstParams.Add(rule.data);

                sQuery = count == 0 ? qRule : String.Format("{0} {1} {2}", sQuery, ConcatOp(mulFilter.groupOp), qRule);

                count++;
            }

            respQry.Query = sQuery;

            return respQry;
        }

        private static string ConcatOp(string sField, int? iPosition = null)
        {
            switch (sField.ToLower())
            {
                case SQL_OPERATOR_AND:
                    return SQL_OPERATOR_AND;
                case SQL_OPERATOR_BW:
                    return String.Format(SQL_OPERATOR_BW_SW, iPosition);
            }

            throw new ArgumentException(ResShared.ERROR_NOT_VALID_OPERATOR);
        }

        private static string SafeName(string field)
        {
            return field;
        }
    }
}
