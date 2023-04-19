using CP.VPOS.Models;
using System.Collections.Generic;

namespace CP.VPOS.Services
{
    internal static class BinService
    {
        internal static string data = @"[
  {
    ""binNumber"": ""365770"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""365771"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""365772"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""365773"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374421"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374422"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""374423"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374424"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""374425"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374426"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374427"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374428"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""375622"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375623"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375624"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375625"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375626"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375627"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375628"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""375629"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""375630"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375631"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""377137"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""377596"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": true,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""377597"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""377598"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""377599"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""400684"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""400742"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""401049"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""401072"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""401622"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""401738"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""402142"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""402277"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""402278"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""402458"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""402459"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""402563"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""402589"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""402590"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""402591"",
    ""bankCode"": ""0205"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""402592"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""402940"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""403082"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""403134"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""403280"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""403360"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""403666"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""403836"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""404308"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""404315"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""404350"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""404591"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""404809"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""404952"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""405051"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""405090"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""405903"",
    ""bankCode"": ""0123"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""405913"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""405917"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""405918"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""405919"",
    ""bankCode"": ""0123"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""406015"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""406281"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""406665"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""406666"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""407814"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""408579"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""408581"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""408625"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""408969"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""409070"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""409071"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""409084"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""409219"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""409364"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""410141"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""410147"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""411156"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""411157"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""411158"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""411159"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""411685"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""411724"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""411942"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""411943"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""411944"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""411979"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""413226"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""413252"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""413382"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""413528"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""413836"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""414070"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""414388"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""414392"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""415514"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""415515"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""415792"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""416275"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""416563"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""416607"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""416757"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""417715"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""418342"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""418343"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""418344"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""418345"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""420092"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""420322"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""420323"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""420324"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""420342"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""420343"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""420556"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""420557"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""421030"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""421086"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""422376"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""422629"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""423002"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""423091"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""423478"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""423833"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""424360"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""424361"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""424909"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""424927"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""424935"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""425669"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""425846"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""425847"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""425848"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""425849"",
    ""bankCode"": ""0135"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""426886"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""426887"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""426888"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""426889"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""427308"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""427311"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""427314"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""427315"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""427707"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""428220"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""428221"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""428240"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""428462"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""428945"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""431024"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""432071"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 10
  },
  {
    ""binNumber"": ""432072"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 10
  },
  {
    ""binNumber"": ""432154"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""432284"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""432285"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""432951"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""432952"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""432953"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""432954"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""434528"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""434529"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""434530"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""434572"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""434724"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""435508"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""435509"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""435627"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""435628"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""435629"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""435653"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""438040"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""440247"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""440273"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""440293"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""440294"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""440295"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""441007"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""441075"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""441206"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""441341"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""442106"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""442395"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""442671"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""444676"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""444677"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""444678"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""446212"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""447503"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""447504"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""447505"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""448472"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""450634"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""450803"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""450918"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""451454"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""453145"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""453146"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""453147"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""453148"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""453149"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""454318"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""454358"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""454359"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""454360"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""454671"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""454672"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""454673"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""454674"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""455359"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""455571"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""455645"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""459026"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""459068"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""459252"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""459333"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""460345"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""460346"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""460347"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""460925"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""460952"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""462274"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""466280"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""466282"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""466283"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""466284"",
    ""bankCode"": ""0124"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""467293"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""467294"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""467295"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""468791"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""468793"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""468794"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""468795"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""468796"",
    ""bankCode"": ""0146"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""468797"",
    ""bankCode"": ""0146"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""469180"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""469181"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""469188"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""469884"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""470954"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""472914"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""472915"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""474151"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""474853"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""474854"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""476619"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""476662"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""479227"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479610"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""479612"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""479632"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479633"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479679"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""479680"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479681"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479682"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479794"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""479795"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""479908"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""479909"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""479915"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479916"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479917"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""480296"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""482465"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""482489"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""482490"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""482491"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""483602"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""483612"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""483673"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""483674"",
    ""bankCode"": ""0205"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""485061"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""487074"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""487075"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489401"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""489455"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489456"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489458"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489478"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489494"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489495"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""489496"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""490175"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""490805"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""490806"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""490807"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""490808"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""490983"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""491005"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""491205"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""491206"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""492094"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""492095"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""492128"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""492130"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""492131"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""492186"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""492187"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""492193"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""493840"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""493841"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""493845"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""493846"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""494064"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""494314"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""496019"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""497022"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""498724"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""498725"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""498749"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""498852"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""499821"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""499850"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""499851"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""499852"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""499853"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""504166"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""508129"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""510005"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""510056"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""510063"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510118"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510119"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510138"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510139"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510151"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""510152"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""510221"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""511583"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""511660"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""511758"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""511783"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""511885"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""512017"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""512117"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""512360"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""512440"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""512595"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""512651"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""512753"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""512754"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 10
  },
  {
    ""binNumber"": ""512803"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""513662"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""514140"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""514915"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""514924"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""515456"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516308"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""516458"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""516643"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516731"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516740"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""516840"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""516841"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""516846"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516914"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516932"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""516943"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""516944"",
    ""bankCode"": ""0146"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516961"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517040"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517041"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517042"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517047"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""517048"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517049"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""517343"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""517946"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""518896"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""519261"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""519324"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""519399"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""519753"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""519780"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520017"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""520019"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520097"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520180"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""520303"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520922"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520932"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""520940"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""520988"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""521022"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""521378"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""521394"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""521807"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""521824"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""521825"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""521836"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""521848"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""521942"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""522075"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""522204"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""522221"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""522240"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""522241"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""522347"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""522356"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""522362"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""522441"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""522566"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""522576"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""523515"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""523529"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""524346"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""524347"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 10
  },
  {
    ""binNumber"": ""524659"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""524677"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""524839"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""524840"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""525312"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""525314"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""525404"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""525413"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""525795"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""525864"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""526289"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""526290"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""526911"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""526952"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""526955"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""526973"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""526975"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""527083"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527369"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527383"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527396"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527682"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""527765"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""528064"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""528208"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""528246"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""528293"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""528920"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""528939"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 6
  },
  {
    ""binNumber"": ""528956"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""529545"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""529572"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""529876"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""530818"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""530853"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""530866"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""530905"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""531102"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""531157"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""531369"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""531401"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""531531"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""532443"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""532581"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""532813"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""533154"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""533169"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""533293"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""533330"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""533796"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""533803"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""534253"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""534261"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""534264"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""534538"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""534563"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""534567"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""534653"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""534981"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""535137"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535217"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535280"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""535435"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""535449"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535488"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""535514"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535576"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535601"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""535602"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""535843"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535881"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""536255"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537058"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""537475"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""537504"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537567"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537719"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""537829"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""538121"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""538124"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""538139"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""538196"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""539957"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""540024"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540025"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540036"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540037"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540045"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540046"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540061"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540062"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540063"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540118"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540122"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540129"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""540130"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""540134"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""540435"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""540643"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""540667"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""540668"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""540669"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""540709"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""541865"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542029"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542030"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542117"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""542119"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""542254"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""542259"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542374"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""542404"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""542605"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542798"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""542804"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""542965"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542967"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""543081"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""543358"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""543427"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""543738"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""543771"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""544078"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""544836"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""545103"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""545120"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""545124"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""545148"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""545183"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""545254"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""545616"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""545847"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""546764"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""546957"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""547234"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""547244"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""547287"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""547311"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""547564"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""547567"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""547765"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""547800"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""547985"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""548232"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""548237"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""548819"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""548935"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""549208"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""549294"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""549449"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""549539"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""549624"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""549839"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""549997"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 9
  },
  {
    ""binNumber"": ""549998"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""550074"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""550449"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""550472"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""550473"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""550478"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""552096"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""552101"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""552143"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""552207"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""552608"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""552609"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""552610"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""552645"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""552659"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""552679"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""552879"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""553056"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 10
  },
  {
    ""binNumber"": ""553058"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""553090"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""553130"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""554297"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""554301"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""554548"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""554570"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""554960"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""556030"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""556031"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""556033"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""556034"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""556665"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""557023"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""557113"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""557829"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""558443"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558446"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558448"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558460"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558485"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558514"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""558593"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""558699"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""559056"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""559096"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""589004"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 7
  },
  {
    ""binNumber"": ""589283"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""589311"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""589318"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""589713"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""603072"",
    ""bankCode"": ""0135"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""603322"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""603343"",
    ""bankCode"": ""0103"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""603344"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603650"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603797"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""606043"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""606329"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""622403"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": true,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""627510"",
    ""bankCode"": ""0203"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""638888"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""639001"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""639004"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""650052"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650082"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650161"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""650170"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""650173"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""650456"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650987"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": true,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""654997"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""657366"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""657998"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""670606"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""670610"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""670670"",
    ""bankCode"": ""0124"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""670868"",
    ""bankCode"": ""0146"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""671121"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""671155"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676123"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""676124"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""676166"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""676255"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""676258"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""676283"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""676366"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676401"",
    ""bankCode"": ""0123"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""676402"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676406"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676460"",
    ""bankCode"": ""0135"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676651"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""676827"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""676832"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""677047"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""677193"",
    ""bankCode"": ""0123"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": true,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""677238"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""677451"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""685800"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""979202"",
    ""bankCode"": ""0111"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""979203"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""979204"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""979206"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""979207"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": true,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""979208"",
    ""bankCode"": ""0046"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979209"",
    ""bankCode"": ""0015"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979210"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""979211"",
    ""bankCode"": ""0059"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979212"",
    ""bankCode"": ""0012"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""979213"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979215"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": true,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""979216"",
    ""bankCode"": ""0205"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979217"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""979218"",
    ""bankCode"": ""0206"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979223"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""979227"",
    ""bankCode"": ""0203"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979233"",
    ""bankCode"": ""0064"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979236"",
    ""bankCode"": ""0062"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""979240"",
    ""bankCode"": ""0135"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979241"",
    ""bankCode"": ""0067"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""979242"",
    ""bankCode"": ""0099"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979243"",
    ""bankCode"": ""0134"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979244"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""979246"",
    ""bankCode"": ""0143"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979264"",
    ""bankCode"": ""0032"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979280"",
    ""bankCode"": ""0010"",
    ""cardType"": 0,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""113012"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113015"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""11301500"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113032"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""11303200"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113059"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113064"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113134"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113334"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 6,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""113336"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 6,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""115032"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""115067"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""115134"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""340000"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""370000"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""374429"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""375632"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""377138"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""377600"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""40227801"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""403998"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""406386"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""409622"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""411160"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""41556501"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""415956"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""416350"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""416394"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""417716"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""423480"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""423495"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""431379"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""434524"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""434728"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""434742"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""434839"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""434909"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""438331"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""439561"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""439757"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""439845"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""439847"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""439848"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""439849"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""440274"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""441076"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""441077"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""454314"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""456944"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""463359"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""466260"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 1
  },
  {
    ""binNumber"": ""468973"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""473956"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""473957"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""473998"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""474108"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""474215"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""474340"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""474508"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""477959"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""478551"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479620"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""479660"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479661"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479662"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""479671"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""487146"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""487147"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""487148"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""487149"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""489375"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""493837"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""51023900"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""512618"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516741"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""516789"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""516835"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""516843"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""517601"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""518358"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""52102280"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""523034"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""524627"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""525329"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""525382"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""527149"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527192"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527284"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527327"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""527737"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""528133"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""528926"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""533913"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""533973"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""534565"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535177"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""535241"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535270"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535279"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535429"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""535735"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""535748"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""535775"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""536180"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""536503"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537470"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""537500"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537518"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537537"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""537548"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""537833"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""539803"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""541440"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""541786"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""542941"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""543624"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""547772"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""549394"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""550383"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""552427"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""554253"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""554254"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""554422"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""555467"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""555574"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""555636"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""555660"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""557370"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""557374"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""559289"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""601050"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""601382"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""601912"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""601913"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603006"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603073"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603126"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""603323"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""620003"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""622404"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""627162"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""627463"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""627511"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""627769"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""650083"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""650092"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650175"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""650268"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 2
  },
  {
    ""binNumber"": ""650271"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""650273"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650274"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""650990"",
    ""bankCode"": ""0064"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 5
  },
  {
    ""binNumber"": ""670640"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""671217"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""676578"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""677055"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""677397"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""690750"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""900002"",
    ""bankCode"": ""0143"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900003"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900004"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""900006"",
    ""bankCode"": ""0206"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900009"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900010"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900011"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900012"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900013"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900014"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900015"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900016"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900018"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900019"",
    ""bankCode"": ""0046"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 0
  },
  {
    ""binNumber"": ""900021"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900022"",
    ""bankCode"": ""0032"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900025"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900026"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900027"",
    ""bankCode"": ""0010"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""900105"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""9001203"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 3,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""900135"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""9021001"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 0,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""940001"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""955100"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""966666"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""97920301"",
    ""bankCode"": ""0111"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""979205"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 3
  },
  {
    ""binNumber"": ""979214"",
    ""bankCode"": ""0123"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 4
  },
  {
    ""binNumber"": ""979219"",
    ""bankCode"": ""0146"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979220"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979224"",
    ""bankCode"": ""0099"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979225"",
    ""bankCode"": ""0103"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979226"",
    ""bankCode"": ""0203"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979228"",
    ""bankCode"": ""0124"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979237"",
    ""bankCode"": ""0134"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979238"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979245"",
    ""bankCode"": ""0012"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": 8
  },
  {
    ""binNumber"": ""979252"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": -1,
    ""commercialCard"": false,
    ""cardProgram"": 11
  },
  {
    ""binNumber"": ""97925251"",
    ""bankCode"": ""0135"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979256"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979263"",
    ""bankCode"": ""0067"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""97926500"",
    ""bankCode"": ""0015"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979267"",
    ""bankCode"": ""0205"",
    ""cardType"": 1,
    ""cardBrand"": -1,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979268"",
    ""bankCode"": ""0059"",
    ""cardType"": 1,
    ""cardBrand"": 2,
    ""commercialCard"": false,
    ""cardProgram"": -1
  },
  {
    ""binNumber"": ""979300"",
    ""bankCode"": ""0062"",
    ""cardType"": 1,
    ""cardBrand"": 5,
    ""commercialCard"": false,
    ""cardProgram"": 3
  }
]";

        private static List<CreditCardBinQueryResponse> dataList = null;

        internal static List<CreditCardBinQueryResponse> GetBinList()
        {
            return dataList = dataList ?? Newtonsoft.Json.JsonConvert.DeserializeObject<List<CreditCardBinQueryResponse>>(data);
        }
    }
}
