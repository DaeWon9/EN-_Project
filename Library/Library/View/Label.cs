using System;

namespace Library.View
{
    class Label : Message
    {
        public void PrintLibraryLabel(bool isClear = true) // 중복되는거 빼기, 제한조건 안내
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("             *     ■     ■■■  ■■■   ■■■      ■■    ■■■   ■    ■     *              ");
            Console.WriteLine("            *      ■       ■    ■   ■  ■   ■    ■  ■   ■   ■   ■  ■       *             ");
            Console.WriteLine("           *       ■       ■    ■■■   ■■■    ■■■■  ■■■      ■          *            ");
            Console.WriteLine("            *      ■       ■    ■   ■  ■   ■   ■    ■  ■   ■     ■         *             ");
            Console.WriteLine("             *     ■■■ ■■■  ■■■   ■    ■  ■    ■  ■    ■    ■        *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintMemberLoginLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                         < 회 원 로 그 인 >                          *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *                ● 가입하신 ID 와 PASSWORD를 적어주세요                  *             ");
            Console.WriteLine("           *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *            ");
            Console.WriteLine("            *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }


        public void PrintAdimistratorLoginLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 관 리 자 로 그 인 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *                ● 관리자 ID 와 PASSWORD를 적어주세요                    *             ");
            Console.WriteLine("           *                 ● ID       : (영어 & 숫자 6 ~ 10 글자)                   *            ");
            Console.WriteLine("            *                ● PASSWORD : (영어 & 숫자 6 ~ 10 글자)                  *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintMemberSignUpLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 회 원 가 입 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 해당칸에 정보를 입력해주세요                                     *             ");
            Console.WriteLine("           *      ● ID, PASSWORD : (영어 & 숫자 6 ~ 10 글자)                          *            ");
            Console.WriteLine("            *     ● 주소 : (시|도) + (시|군|구) + (읍|면|동) 까지 적어주세요         *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintBookSearchLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 검 색 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *  ● ↑↓ 방향키로 이동 후 ENTER를 누르고 입력하세요 (원하는 옵션입력)   *             ");
            Console.WriteLine("           *   ● 도서가격은 수치보다 낮은, 도서수량은 수치보다 높은 도서를 검색합니다 *            ");
            Console.WriteLine("            *  ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                    *             ");
            Console.WriteLine("             * ● 입력을 완료한 뒤에 <검색하기> 에서 ENTER를 눌러주세요              *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintMemberSearchLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 회 원 검 색 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *   ● ↑↓ 방향키로 이동 후 ENTER를 누르고 입력하세요 (원하는 옵션입력)  *             ");
            Console.WriteLine("           *    ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                    *            ");
            Console.WriteLine("            *   ● 입력을 완료한 뒤에 <검색하기> 에서 ENTER를 눌러주세요              *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }


        public void PrintBorrowBookModeLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 대 여 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *    ● ↑↓ 방향키로 이동하며 ENTER키를 눌러 대여방식을 선택해주세요     *             ");
            Console.WriteLine("           *     ● 바로 대여    : 도서관에 보유중인 책정보를 바탕으로 대여가능        *            ");
            Console.WriteLine("            *    ● 검색 후 대여 : 사용자가 검색한 책정보를 바탕으로 대여가능         *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintBorrowBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 대 여 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 대여하고자하는 도서아이디를 적어주세요                           *             ");
            Console.WriteLine("           *      ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다         *            ");
            Console.WriteLine("            *     ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                 *             ");
            Console.WriteLine("             *    ● 입력을 완료한 뒤에 <대여하기> 에서 ENTER를 눌러주세요           *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintReturnBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 반 납 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 반납하고자하는 도서아이디를 적어주세요                           *             ");
            Console.WriteLine("           *      ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다         *            ");
            Console.WriteLine("            *     ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                 *             ");
            Console.WriteLine("             *    ● 입력을 완료한 뒤에 <반납하기> 에서 ENTER를 눌러주세요           *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintCheckBorrowedBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 대 여 도 서 확 인 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *           ● 대여중인 도서 목록입니다.                                  *             ");
            Console.WriteLine("           *            ● 남은기간이 4일 미만일경우 노란색으로 표시됩니다.            *            ");
            Console.WriteLine("            *           ● 남은기간이 1일 미만일경우 빨간색으로 표시됩니다.           *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintModifyMemberInformationLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 회 원 정 보 수 정 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *       ● 좌측은 회원님의 등록되어있는 정보입니다.                       *             ");
            Console.WriteLine("           *        ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다       *            ");
            Console.WriteLine("            *       ● 입력 도중 ESC를 누르면 입력을 취소할 수 있습니다               *             ");
            Console.WriteLine("             *      ● 수정을 원하는 내용 입력 후 ENTER를 눌러주세요                 *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          < 회 원 정 보 >                                           ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
        public void PrintSelectModifyBookModeLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 도 서 정 보 수 정 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *    ● ↑↓ 방향키로 이동하며 ENTER키를 눌러 수정방식을 선택해주세요     *             ");
            Console.WriteLine("           *     ● 바로 수정    : 도서 아이디를 알고있을경우 바로 수정가능            *            ");
            Console.WriteLine("            *    ● 검색 후 수정 : 검색한 도서정보를 바탕으로 수정가능                *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintSelectModifyBookIDLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 도 서 정 보 수 정 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 수정하고자하는 도서아이디를 적어주세요                           *             ");
            Console.WriteLine("           *      ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다         *            ");
            Console.WriteLine("            *     ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                 *             ");
            Console.WriteLine("             *    ● 입력을 완료한 뒤에 <수정하기> 에서 ENTER를 눌러주세요           *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintModifyBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 도 서 정 보 수 정 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *       ● 상단의 정보는 선택하신 도서의 정보입니다                       *             ");
            Console.WriteLine("           *        ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다       *            ");
            Console.WriteLine("            *       ● 입력 도중 ESC를 누르면 입력을 취소할 수 있습니다               *             ");
            Console.WriteLine("             *      ● 수정을 원하는 내용 입력 후 ENTER를 눌러주세요                 *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          < 도 서 정 보 >                                           ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintAddBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 추 가 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 추가하고자하는 도서정보를 적어주세요 (모두입력)                  *             ");
            Console.WriteLine("           *      ● ↑↓ 방향키로 옵션 이동 후 ENTER를 누르면 입력 가능합니다         *            ");
            Console.WriteLine("            *     ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                 *             ");
            Console.WriteLine("             *    ● 입력을 완료한 뒤에 <추가하기> 에서 ENTER를 눌러주세요           *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintMemberManagementLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 회 원 정 보 수 정 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *       ● 등록되어있는 회원님들의 정보입니다                             *             ");
            Console.WriteLine("           *        ● 특정 회원의 ID를 입력하시면 회원정보를 수정할 수 있습니다       *            ");
            Console.WriteLine("            *                                                                         *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintSelectManagementMemberModeLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 회 원 정 보 관 리 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 아래는 등록되어있는 모든 회원님들의 정보입니다                   *             ");
            Console.WriteLine("           *      ● ↑↓ 방향키로 이동하며 ENTER키를 눌러 옵션을 선택해주세요         *            ");
            Console.WriteLine("            *     ● 회원정보 검색 : 원하는 옵션으로 회원정보 검색 가능               *             ");
            Console.WriteLine("             *    ● 회원정보 수정 : 회원아이디 입력시 해당 회원의 정보 수정가능     *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintAdministratorCheckBorrowedBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 대 여 도 서 상 황 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *           ● 검색된 대여중인 도서 목록입니다.                           *             ");
            Console.WriteLine("           *            ● 남은기간이 4일 미만일경우 노란색으로 표시됩니다.            *            ");
            Console.WriteLine("            *           ● 남은기간이 1일 미만일경우 빨간색으로 표시됩니다.           *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintAdministratorCheckBorrowedBookModeLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 대 여 도 서 상 황 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *        ● 회원별 대여중인 도서 목록입니다.                              *             ");
            Console.WriteLine("           *         ● 조회를 원하는 도서아이디 OR 회원아이디를 입력해주세요          *            ");
            Console.WriteLine("            *        ● 남은기간이 4일 미만일경우 노란색으로 표시됩니다.              *             ");
            Console.WriteLine("             *       ● 남은기간이 1일 미만일경우 빨간색으로 표시됩니다.             *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintLogLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 로 그 현 황 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *        ● 그동안 기록된 로그 현황입니다.                                *             ");
            Console.WriteLine("           *         ● 바탕화면에 자동으로 Log.txt 파일이 저장됩니다.                 *            ");
            Console.WriteLine("            *                                                                         *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintExplainSearchBookByNaverLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                       < 네 이 버 도 서 검 색 >                      *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *        ● 네이버로 검색을 하는 기능입니다.                              *             ");
            Console.WriteLine("           *         ● 검색할 도서명과, 도서권수(1~99)를 적어주세요                   *            ");
            Console.WriteLine("            *        ● 모두 입력하신 후 < 검색하기 > 에서 ENTER 눌러주세요           *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }

        public void PrintSearchBookByNaverLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                       < 네 이 버 도 서 검 색 >                      *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *    ● 네이버로 검색한 결과입니다.                                       *             ");
            Console.WriteLine("           *     ● 검색된 정보를 바탕으로 도서를 추가하시려면 번호를 입력해주세요     *            ");
            Console.WriteLine("            *    ● 입력을 마치신 후 < 추가하기 > 에서 ENTER 눌러주세요               *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
    }
}
