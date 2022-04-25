using System;

namespace Library.View
{
    class Label : Message
    {
        public void PrintLibraryLabel(bool isClear = true)
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
            Console.WriteLine("              *                           < 도 서 찾 기 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *  ● ↑↓ 방향키로 이동 후 ENTER를 누르고 입력해주세요 (원하는 옵션입력) *             ");
            Console.WriteLine("           *   ● 도서가격은 수치보다 낮은, 도서수량은 수치보다 높은 도서를 검색합니다 *            ");
            Console.WriteLine("            *  ● 검색 도중 ESC를 누르면 입력을 취소할 수 있습니다                    *             ");
            Console.WriteLine("             * ● 입력을 완료한 뒤에 <검색하기> 에서 ENTER를 눌러주세요              *              ");
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
            Console.WriteLine("            *      ● ↑↓ 방향키로 이동하며 대여방식을 선택해주세요                  *             ");
            Console.WriteLine("           *       ● 바로 대여    : 도서관에 보유중인 책정보를 바탕으로 대여가능      *            ");
            Console.WriteLine("            *      ● 검색 후 대여 : 사용자가 검색한 책정보를 바탕으로 대여가능       *             ");
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

        public void PrintModificateMemberInformationLabel(bool isClear = true)
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
            Console.WriteLine("               < 회 원 정 보 >               |                  < 수 정 할 정 보 >                  ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintModificateBookInformationLabel(bool isClear = true)
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
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          < 도 서 정 보 >                                           ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintAdministratorCheckBorrowedBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                        < 대 여 도 서 상 황 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *           ● 회원별 대여중인 도서 목록입니다.                           *             ");
            Console.WriteLine("           *            ● 남은기간이 4일 미만일경우 노란색으로 표시됩니다.            *            ");
            Console.WriteLine("            *           ● 남은기간이 1일 미만일경우 빨간색으로 표시됩니다.           *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                뒤로가기 : ESC      ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }

        public void PrintAddBookLabel(bool isClear = true)
        {
            if (isClear)
                Console.Clear();
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
            Console.WriteLine("              *                           < 도 서 추 가 >                           *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *     ● 추가하고자하는 도서정보를 적어주세요                             *             ");
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
            Console.WriteLine("              *                        < 회 원 정 보 관 리 >                        *               ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("            *       ● 등록되어있는 회원님들의 정보입니다                             *             ");
            Console.WriteLine("           *        ● 특정 회원의 ID를 검색하시면 회원정보를 수정할 수 있습니다       *            ");
            Console.WriteLine("            *                                                                         *             ");
            Console.WriteLine("             *                                                                       *              ");
            Console.WriteLine("              *                                                                     *               ");
            Console.WriteLine("               *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *                ");
        }
    }
}
