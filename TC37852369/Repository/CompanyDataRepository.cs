using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Services.Encoder;

namespace TC37852369.Repository
{
    public class CompanyDataRepository
    {
        StringEncoder stringEncoder = new StringEncoder();
        public async Task<bool> EditCompanyData(string address, string companyName, string email, 
            string phoneNumber, string webPageAddress, string companyLogo, string emailSurename,
            string emailPassword)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;

            DocumentReference docRef = db.Collection("CompanyData").Document("CompanyData");

            string encodedEmailPassword = StringEncoder.ReturnEncryptedPassword(emailPassword);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Address",            address                 },
                { "CompanyLogo",        companyLogo             },     
                { "CompanyName",        companyName             },
                { "Email",              email                   },
                { "PhoneNumber",        phoneNumber             },
                { "WebPageAddress",     webPageAddress          },
                { "EmailSurename",     emailSurename            },
                { "EmailPassword",     encodedEmailPassword     },

            };
            try
            {
                cts.CancelAfter(10000);
                await docRef.SetAsync(user, null, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\r\nUpload canceled.\r\n");
                return false;
            }
            catch (AggregateException)
            {
                Console.WriteLine("\r\nUpload failed.\r\n");
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("\r\nUpload failed.\r\n");
                return false;
            }
            return true;
        }

        public async Task<CompanyData> GetCompanyData()
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            CompanyData companyData = null;
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            Query CompanyDataQuery = db.Collection("CompanyData");
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken cancellationToken = cts.Token;
            QuerySnapshot CompanyDataQuerySnapshot;
            try
            {
                cts.CancelAfter(10000);
                CompanyDataQuerySnapshot = await CompanyDataQuery.GetSnapshotAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\r\nDownload canceled.\r\n");
                return null;
            }
            catch (AggregateException)
            {
                Console.WriteLine("\r\nDownload failed.\r\n");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("\r\nDownload failed.\r\n");
                return null;
            }

            foreach (DocumentSnapshot documentSnapshot in CompanyDataQuerySnapshot.Documents)
            {
                Dictionary<string, object> companyDataDictionary = documentSnapshot.ToDictionary();
               

                companyData = new CompanyData(
                        companyDataDictionary["Address"].ToString(),
                        companyDataDictionary["CompanyName"].ToString(),
                        companyDataDictionary["Email"].ToString(),
                        companyDataDictionary["PhoneNumber"].ToString(), 
                        companyDataDictionary["WebPageAddress"].ToString(),
                        companyDataDictionary["CompanyLogo"].ToString(),
                        companyDataDictionary["EmailSurename"].ToString(),
                        StringEncoder.ReturnDecryptedPassword(companyDataDictionary["EmailPassword"].ToString())
                    );
            }
            return companyData;

        }

    }
}
