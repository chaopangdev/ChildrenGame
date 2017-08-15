using System;
using System.Net.Http;
using ChildrenGame.Models;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using log4net;
using System.Threading;

namespace ChildrenGame.Services
{
    /// <summary>
    /// An external API implementation game helper service, loading parameters from external API and uploading result to it. 
    /// </summary>
    public class ExternalAPIGameHelperService : IGameHelperService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ExternalAPIGameHelperService).Name);

        //URI of the external API, defined in config file.
        private readonly string _URI = ConfigurationManager.AppSettings.Get("ChildrenGameAPIBaseAddress");

        /// <summary>
        /// Load game parameters from remote API asynchronously
        /// </summary>
        /// <returns>Game parameter object, null if loading failed</returns>
        public async Task<GameParameter> InitializeGameAsync(CancellationToken cancellationToken)
        {
            GameParameter gameParameter = null;

            if (String.IsNullOrEmpty(_URI))
                return null;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    _logger.Debug("Start calling third party API to load parameters...");
                    HttpResponseMessage response = await httpClient.GetAsync(_URI, cancellationToken);
                    _logger.DebugFormat("Load parameter completed with status code {0}", response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        gameParameter = await response.Content.ReadAsAsync<GameParameter>();
                        _logger.DebugFormat("Parameter loaded : {0}", gameParameter.ToString());
                    }
                }
            }
            catch (WebException we)
            {
                _logger.Error("Error in calling external API", we);
            }
            catch (OperationCanceledException oe)
            {
                _logger.Error("Operation is cancelled", oe);
                throw;
            }
            return gameParameter;
        }

        /// <summary>
        /// Send the game result to remote API asynchronously
        /// </summary>
        /// <param name="gameResult">game result</param>
        /// <returns>send successfull or not</returns>
        public async Task<bool> HandleGameResultAsync(GameResult gameResult, CancellationToken cancellationToken)
        {
            bool isSendSuccessful = false;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    _logger.Debug("Start calling third party API to upload result...");
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(_URI + gameResult.GameID, gameResult, cancellationToken);
                    _logger.DebugFormat("Upload result completed with status code {0}", response.StatusCode);
                    if (response.IsSuccessStatusCode)
                        isSendSuccessful = true;
                }
            }
            catch (WebException we)
            {
                _logger.Error("Error in calling external API", we);
            }
            catch (OperationCanceledException oe)
            {
                _logger.Error("Operation is canceled", oe);
                throw;
            }
            return isSendSuccessful;
        }
    }
}