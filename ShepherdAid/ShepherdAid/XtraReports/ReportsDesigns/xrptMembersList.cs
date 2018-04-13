using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using ShepherdAid.XtraReports.Datasets;

namespace ShepherdAid.XtraReports.ReportsDesigns
{
    public partial class xrptMembersList : DevExpress.XtraReports.UI.XtraReport
    {
        public xrptMembersList(int id)
        {
            InitializeComponent();
            this.dsetMembersList1.EnforceConstraints = false;
            this.reportMembersListTableAdapter.Fill(this.dsetMembersList1.ReportMembersList, id);
        }

    }
}
