using Assignment_1.model;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Assignment_1.view
{
    class CreateAccount : View
    {
        //Content Value and Content Title
        private String[] ContentValue = { "", "", "", "", ""};
        private String[] Content = { "First Name : ", "Last Name : ", "Address : ", "Phone : ", "Email : " };

        // Loadd Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();
        //Account ID start number
        private int txtId = 100001;

        //DataLine  save all the txt by Line (this is temporary variable)
        private Byte[] txtTextInput;

        //Email Details:
        private const string replyEmail = "BankAss1.noreply@yahoo.com";
        private const string replyPass = "hzdgujfcmzhuvnov";

        //Finish Response of the (y/n) statement
        private Boolean finishResponse = true;

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            write.WriteHeading("CREATE A NEW ACCOUNT");
            write.makingContentBorder(4, 6);
            write.WriteSubheading("ENTER THE DETAILS", 3);
            //for loop to print the content
            for (int i = 0; i < Content.Length; i++)
            {
                write.WriteAt(Content[i] + ContentValue[i], 2, 5 + i);
            }
            //To go back to the first content
            write.WriteAt(Content[0], 2, 5);

            //While loop to keep checking user input details
            while (readInputForUser())
            {
                write.ClearAtY(12);
                write.WriteAt("Please Check Your Input", 0, 12);
            }
            //Let the user recheck details once more
            write.ClearAtY(12);
            write.WriteAt("Is the Information Correct? (y/n)", 0, 12);

            //While loop to keep checking user input either [y] or [n]
            while (finishResponse)
            {
                ConsoleKeyInfo readKeyFinal = Console.ReadKey(true);
                if (readKeyFinal.Key == ConsoleKey.Y)
                {
                    sendMail(ContentValue[4]);
                    write.ClearAtY(12);
                    write.WriteAt("Creating Account Success", 0, 12);
                    write.WriteAt("Please enter any key to continue...", 0, 13);
                    Console.ReadKey();
                    Console.Clear();
                    View nextView = new MainMenu();
                    nextView.Load();
                }
                else if (readKeyFinal.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    this.Load();
                }
            }
        }
        //Create Account function
        private void createAccountTxt()
        {
            //File path + ID + Extension
            string fileName = @"..\..\..\account\" + txtId + "" + ".txt";
            try
            {
                //If the ID is duplicated
                while (File.Exists(fileName))
                {
                    //Add the ID
                    //make new File path + IDD + Extension
                    txtId++;
                    fileName = @"..\..\..\account\" + txtId + "" + ".txt";
                }
                //Create file if its unique ID
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file following the tutor format
                    for (int i = 0; i < Content.Length; i++)
                    {
                        txtTextInput = new System.Text.UTF8Encoding(true).GetBytes(Content[i].Replace(" : ", "|") + ContentValue[i] + "\n");
                        fs.Write(txtTextInput, 0, txtTextInput.Length);
                    }
                    txtTextInput = new System.Text.UTF8Encoding(true).GetBytes("AccountNo |" + txtId.ToString() + "\n");
                    fs.Write(txtTextInput, 0, txtTextInput.Length);
                    txtTextInput = new System.Text.UTF8Encoding(true).GetBytes("Balance |0");
                    fs.Write(txtTextInput, 0, txtTextInput.Length);
                }
            }
            catch (Exception Ex)
            {
                //Clear console to write error message and load this class UI
                write.ClearAtY(12);
                write.WriteAt(Ex.Message, 0, 12);
                this.Load();
            }
        }
        private void sendMail(string sendTo)
        {
            //Showing feedback from the system to show that the system still running
            write.ClearAtY(12);
            write.WriteAt("Please Wait. Processing", 0, 12);
            //Creating Mail Message
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(replyEmail);

                //receiver email adress
                mailMessage.To.Add(sendTo);

                //subject of the email
                mailMessage.Subject = "New Account Created for Bank";

                //attach the file
                mailMessage.Body = "Thank you "+ ContentValue[0] + "<br><br>";
                mailMessage.Body = "Below here is your Account Details :<br>*Please do not Share<br><br>";
                for (int i = 0; i < ContentValue.Length; i++)
                    mailMessage.Body = mailMessage.Body + Content[i] + ContentValue[i] + "<br>";
                mailMessage.Body = mailMessage.Body + "<br><br>Thank you for choosing us";
                mailMessage.IsBodyHtml = true;

                //SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com");
                //port number for Yahoo
                smtpClient.Port = 587;
                //credentials to login in to yahoo account
                smtpClient.Credentials = new NetworkCredential(replyEmail, replyPass);
                //enabled SSL
                smtpClient.EnableSsl = true;
                //Send an email
                smtpClient.Send(mailMessage);
                createAccountTxt();
            }
            catch (Exception Ex)
            {
                //Clear console to write error message and load this class UI
                write.ClearAtY(12);
                write.WriteAt("Please Check your Email Input", 0, 12);
                this.Load();
            }
        }
        private Boolean readInputForUser()
        {
            //Boolean loopActive to maintain the loop
            Boolean LoopActive = true;
            //IndexContentValue to see which content are user on. (Based on Content Title)
            int indexContentValue = 0;
            do
            {
                //Prevent any unnecessary key
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
                if (readKeyResult.Key == ConsoleKey.LeftArrow ||
                readKeyResult.Key == ConsoleKey.RightArrow ||
                readKeyResult.Key == ConsoleKey.F1 ||
                readKeyResult.Key == ConsoleKey.F2 ||
                readKeyResult.Key == ConsoleKey.F3 ||
                readKeyResult.Key == ConsoleKey.F4 ||
                readKeyResult.Key == ConsoleKey.F5 ||
                readKeyResult.Key == ConsoleKey.F6 ||
                readKeyResult.Key == ConsoleKey.F7 ||
                readKeyResult.Key == ConsoleKey.F8 ||
                readKeyResult.Key == ConsoleKey.F9 ||
                readKeyResult.Key == ConsoleKey.F10 ||
                readKeyResult.Key == ConsoleKey.Home ||
                readKeyResult.Key == ConsoleKey.F12 ||
                readKeyResult.Key == ConsoleKey.LeftWindows ||
                readKeyResult.Key == ConsoleKey.RightWindows ||
                readKeyResult.Key == ConsoleKey.Tab ||
                readKeyResult.Key == ConsoleKey.Escape)
                { }
                //If they press up arrow to go up in the content
                else if (readKeyResult.Key == ConsoleKey.UpArrow)
                {
                    indexContentValue--;
                    indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                }
                //If they press up arrow to go down in the content
                else if (readKeyResult.Key == ConsoleKey.DownArrow)
                {
                    indexContentValue++;
                    indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                }
                //If they press enter it will go down or submit the form
                else if (readKeyResult.Key == ConsoleKey.Enter)
                {
                    if (indexContentValue == ContentValue.Length - 1)
                    {
                        LoopActive = false;
                    }
                    else
                    {
                        indexContentValue++;
                        indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                    }
                }
                else
                {
                    //To delete and do backspace on the contentValue
                    if (readKeyResult.Key == ConsoleKey.Delete || readKeyResult.Key == ConsoleKey.Backspace)
                    {
                        if (ContentValue[indexContentValue].Length != 0)
                        {
                            ContentValue[indexContentValue] = ContentValue[indexContentValue].Remove(ContentValue[indexContentValue].Length - 1, 1);
                        }
                    }
                    //To add details on content Value
                    else
                    {
                        if(ContentValue[indexContentValue].Length < 40)
                        {
                            ContentValue[indexContentValue] += readKeyResult.KeyChar;
                        }
                        else
                        {
                            write.ClearAtY(12);
                            write.WriteAt("Maximum Input allowed is 40 character", 0, 12);
                        }

                    }
                    //clear UI if there is any changes on contentValue
                    write.ClearAtY(5 + indexContentValue);
                }
                //Rewrite UI if there is any changes on contentValue (similar to React)
                write.WriteAt(Content[indexContentValue] + ContentValue[indexContentValue], 2, 5 + indexContentValue);
            } while (LoopActive);
            //If the input is int (from View Class)
            if (!isNull(ContentValue[0]) &&
                !isNull(ContentValue[1]) &&
                !isNull(ContentValue[2]) &&
                checkInt(ContentValue[3]) &&
                checkEmail(ContentValue[4]))
                return false;
            return true;
        }
        /*
         * Check email format
         * Email format will be tested twice.
         * Email check here and before sending mail
         */
        private Boolean checkEmail(String input)
        {
            if (!new EmailAddressAttribute().IsValid(input) || isNull(input))
                return false;
            return true;
        }
    }
}
