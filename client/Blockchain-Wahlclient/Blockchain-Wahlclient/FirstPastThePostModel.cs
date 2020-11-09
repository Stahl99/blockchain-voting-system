using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace Blockchain_Wahlclient
{
    class FirstPastThePostModel
    {
        private FirstPastThePostForm FPTPform;

        public FirstPastThePostModel()
        {
        }

        // Get Reference to the active Form
        private void InitFormReference()
        {
            // get the voting form
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType().Equals(typeof(FirstPastThePostForm)))
                {
                    FPTPform = (FirstPastThePostForm)form;
                    break;
                }
            }
        }

        // verify the voting logic
        public bool VerifyVote(String votingAdress, CheckedItemCollection checkedItems)
        {
            InitFormReference();
            // check if exactly one item is checkd
            if (checkedItems.Count != 1)
            {
                FPTPform.ShowErrorText("Please select a voting option");
                return false;
            }

            // check if the String contains only a HEX value
            if(!OnlyHexInString(votingAdress))
            {
                FPTPform.ShowErrorText("Adress is not a hash adress");
                return false;
            }

            // check if the adress has the correct length
            if (votingAdress.Length != 42)
            {
                FPTPform.ShowErrorText("Adress has incorrect size");
                return false;
            }

            // if everything is ok return true
            return true;
        }

        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }
    }
}
