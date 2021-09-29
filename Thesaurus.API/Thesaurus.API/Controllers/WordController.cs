using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Interface;
using Thesaurus.Api.Helper;

namespace Thesaurus.Api.Controllers
{
    [Route("Thesaurus/Word")]
    public class WordController : Controller
    {
        private readonly IWordLogic _wordLogic;        

        public WordController(IWordLogic wordLogic)
        {
            _wordLogic = wordLogic;            
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWords([FromBody] Request request)
        {
            if (request.Words == null || request.Words.Count < 1)
            {
                return BadRequest("Parameters cannot be empty !");
            }

            try
            {
                if (request.Words.GroupBy(g => g.Primary).Any(g => g.Count() > 1))
                {
                    throw new DataMisalignedException("Duplicate words found in the request");
                }

                await _wordLogic.AddWords(request.Words);

                return StatusCode(201, "Added succesfully");
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException
                                    || ex is DataMisalignedException)
            {                
                return Problem(ex.Message);
            }
        }

        [HttpGet("get/{word}", Name = "GetWord")]
        public IActionResult Get([FromRoute] string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Parameters cannot be empty !");
            }

            try
            {
                var response = _wordLogic.FindWord(word);

                IDictionary<string, Uri> synop = null;

                foreach (var text in response)
                {
                    synop = new Dictionary<string, Uri>();

                    foreach (var dic in text.Synonyms)
                    {
                        synop.Add(dic.Key, LinkHelper.CreateLink(Url, "GetWord", dic.Key));
                    }
                    text.Synonyms = synop;
                    synop = null;
                }

                return Ok(response);
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException)
            {                
                return StatusCode(500);
            }
        }

        [HttpGet("get", Name = "GetAll")]
        public IActionResult GetAll([FromQuery] int currentPage, int pageSize)
        {           
            try
            {
                var response = _wordLogic.GetAll(currentPage, pageSize);
                var outputWordList = new Dictionary<string, Uri>();

                foreach (var word in response)
                {
                    if (!outputWordList.Any(x => x.Key == word.Primary))
                    {
                        outputWordList.Add(word.Primary, LinkHelper.CreateLink(Url, "GetWord", word.Primary));
                    }
                }
                return Ok(new Response
                {
                    Words = outputWordList,
                    NextPage = LinkHelper.CreatePageLink(Url, "GetAll", currentPage + 1, pageSize),
                    PreviousPage = LinkHelper.CreatePageLink(Url, "GetAll",
                                                    currentPage > 1 ? currentPage - 1 : currentPage,
                                                    pageSize)
                });
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException)
            {                
                return Problem(ex.Message);
            }
        }

        [HttpPost("delete/{documentId}")]
        public IActionResult Delete([FromRoute] string documentId)
        {
            if (documentId == null)
            {
                return BadRequest("word id cannot be empty !");
            }

            try
            {
                _wordLogic.DeleteById(documentId);
                return Ok($"Word deleted successfully !!");
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteWords([FromBody] Request request)
        {
            if (request == null)
            {
                return BadRequest("Parameters cannot be empty !");
            }

            try
            {
                _wordLogic.Delete(request.Words.Select(x => x.Id).ToList());

                return Ok($"Deleted successfully !!");
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult Replace([FromBody] Request request)
        {
            if (request == null)
            {
                return BadRequest("Parameters cannot be empty !");
            }

            try
            {
                return Ok(_wordLogic.Update(request.Words.First()));
            }
            catch (Exception ex) when (ex is TimeoutException
                                    || ex is NullReferenceException)
            {
                return Problem(ex.Message);
            }
        }
    }
}
