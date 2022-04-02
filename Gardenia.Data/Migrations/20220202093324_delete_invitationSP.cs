using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class delete_invitationSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var delete_invitation_sp = @"
                create or alter proc delete_invitation
                as

                delete sms
                from SMSVisitorInvitation sms, VisitsLogs v
                where sms.Id != v.SMSVisitorInvitationId
                and sms.Data = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))


                delete qr
                from QRVisitorInvitation qr, VisitsLogs v
                where qr.Id != v.QRVisitorInvitationId
                and qr.Data = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))

            ";

            migrationBuilder.Sql(delete_invitation_sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var delete_invitation_sp = @"
                drop proc delete_invitation
            ";


            migrationBuilder.Sql(delete_invitation_sp);
        }
    }
}
