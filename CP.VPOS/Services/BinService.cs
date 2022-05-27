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
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""365771"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""365772"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""365773"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""374421"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""374422"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""374423"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""374424"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""374425"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""374426"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""374427"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""374428"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375622"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375623"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375624"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375625"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""375626"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""375627"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375628"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375629"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375630"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""375631"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""377137"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""377596"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""377597"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""377598"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""377599"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""400684"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""400742"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""401049"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""401072"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""401622"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""401738"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402142"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""402277"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402278"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402458"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402459"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402563"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402589"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402590"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402591"",
        ""bankCode"": ""0205"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""402592"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""402940"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""403082"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""403134"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""403280"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""403360"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""403666"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""403810"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""403836"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""404308"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""404315"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""404350"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""404591"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""404809"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""404952"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""404990"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""405051"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""405090"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""405903"",
        ""bankCode"": ""0123"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""405913"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""405917"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""405918"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""405919"",
        ""bankCode"": ""0123"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""406015"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""406281"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""406665"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""406666"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""407814"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""408579"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""408581"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""408625"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""408969"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""409070"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""409071"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""409084"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""409219"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""409364"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""410141"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""410147"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""410555"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""410556"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411156"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411157"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411158"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411159"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""411685"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""411724"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411924"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""411942"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411943"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411944"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""411979"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413226"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413252"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413382"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413528"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413583"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""413836"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""414070"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""414388"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""414392"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""415514"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""415515"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""415565"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""415792"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""416275"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""416563"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""416607"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""416757"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""417715"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""418342"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""418343"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""418344"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""418345"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420092"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420322"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420323"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420324"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420342"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420343"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420556"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""420557"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""421030"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""421086"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""422376"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""422629"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423002"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423091"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423277"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423398"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423478"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423667"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""423833"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424360"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424361"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424909"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424927"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424931"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""424935"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""425669"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""425846"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""425847"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""425848"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""425849"",
        ""bankCode"": ""0135"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""426886"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""426887"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""426888"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""426889"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""427308"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""427311"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""427314"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""427315"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""427707"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""428220"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""428221"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""428240"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""428462"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""428945"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""431024"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""432071"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432072"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432154"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432284"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432285"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432951"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432952"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432953"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""432954"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""433383"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""434528"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""434529"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""434530"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""434572"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""434724"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435508"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435509"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435627"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435628"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435629"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""435653"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""438040"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""440247"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""440273"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""440293"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""440294"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""440295"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""440776"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""441007"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""441075"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""441139"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""441206"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""441341"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""442106"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""442395"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""442671"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""444029"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""444676"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""444677"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""444678"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""446212"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""447503"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""447504"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""447505"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""448472"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""450634"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""450803"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""450918"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""451454"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453144"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453145"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453146"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453147"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453148"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""453149"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454318"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454358"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454359"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454360"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454671"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454672"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""454673"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""454674"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""455359"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""455571"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""455645"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""459026"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""459068"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""459252"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""459268"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""459333"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""460345"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""460346"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""460347"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""460925"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""460952"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""461668"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""462274"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""462276"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""462448"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""462449"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""465574"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""466280"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""466281"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""466282"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""466283"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""466284"",
        ""bankCode"": ""0124"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""467293"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""467294"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""467295"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""468791"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""468793"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""468794"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""468795"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""468796"",
        ""bankCode"": ""0146"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""468797"",
        ""bankCode"": ""0146"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""469180"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""469181"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""469188"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""469884"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""470954"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""472914"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""472915"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""474151"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""474823"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""474853"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""474854"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""476619"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""476625"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""476626"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""476662"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479227"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""479610"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479612"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479632"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""479633"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479679"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""479680"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479681"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479682"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""479794"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479795"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479908"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""479909"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479915"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479916"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""479917"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""480296"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""482465"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""482489"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""482490"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""482491"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483602"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483612"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483673"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483674"",
        ""bankCode"": ""0205"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483714"",
        ""bankCode"": ""0205"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""483747"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""485061"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""486567"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""487074"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""487075"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489401"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489455"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489456"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489457"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489458"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489478"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489494"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489495"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""489496"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""490175"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""490805"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""490806"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""490807"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""490808"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""490983"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""491005"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""491205"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""491206"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492094"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492095"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492128"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492130"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492131"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492186"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492187"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""492193"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""493840"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""493841"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""493845"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""493846"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""494063"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""494064"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""494314"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""496019"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""497022"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""498724"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""498725"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""498749"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""498852"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""499821"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""499850"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""499851"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""499852"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""499853"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""504166"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""508129"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510005"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510054"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510056"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510063"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510118"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""510119"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""510138"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510139"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510151"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510152"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""510221"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""511583"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""511660"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""511758"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""511783"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""511885"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512017"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512117"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512360"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512440"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512446"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512595"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512651"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512753"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512754"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""512803"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""513662"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""514140"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""514915"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""514924"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""515456"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""515755"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""515865"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""515895"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516308"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516458"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516643"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516731"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516740"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516742"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516840"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516841"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516846"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516888"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516914"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516932"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516943"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516944"",
        ""bankCode"": ""0146"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""516961"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517040"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517041"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517042"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517047"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517048"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517049"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517343"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""517946"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""518896"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""519261"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""519324"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""519399"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""519753"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""519780"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""520017"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520019"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520097"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520180"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520303"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520909"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520922"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520932"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520940"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""520988"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521022"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521368"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521378"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521394"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521584"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521807"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521824"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521825"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521832"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521836"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521848"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""521942"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522075"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522204"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522221"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522240"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522241"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522265"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522347"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522356"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522362"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522441"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""522517"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522566"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""522576"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""523515"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""523529"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""524346"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""524347"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""524659"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""524677"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""524839"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""524840"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525312"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525314"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525404"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525413"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525795"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""525864"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526289"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526290"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526911"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526952"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526955"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526973"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""526975"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527026"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527083"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527369"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527383"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527396"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527657"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527682"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""527765"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528064"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528208"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528246"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528293"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528825"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528920"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528939"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""528956"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""529545"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""529572"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""529876"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""530818"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""530853"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""530866"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""530905"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""531102"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""531157"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""531369"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""531401"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""531531"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""532443"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""532581"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""532813"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""533154"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""533169"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""533293"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""533330"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""533796"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""533803"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534253"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534261"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534264"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534538"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534563"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534567"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534653"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""534981"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535137"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""535217"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535280"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""535435"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535449"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535488"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""535514"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535576"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535601"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535602"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""535843"",
        ""bankCode"": ""0143"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""535881"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""536255"",
        ""bankCode"": ""0146"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""537058"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""537475"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""537504"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""537567"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""537719"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""537829"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""538121"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""538124"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""538139"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""538196"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""539605"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""539957"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540024"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540025"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""540036"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540037"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""540045"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540046"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540061"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540062"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540063"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540118"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540122"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540129"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540130"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540134"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540226"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540227"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540435"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540643"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540667"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540668"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540669"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""540709"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""541865"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542029"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542030"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542117"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542119"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542254"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542259"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542374"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542404"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""542605"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542798"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542804"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542965"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""542967"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543081"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543358"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543400"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543427"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543738"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""543771"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""544078"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""544836"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545102"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545103"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545120"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545124"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545148"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545183"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545254"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545616"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""545847"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""546001"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""546764"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""546957"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""547234"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547244"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547287"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547302"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547311"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547564"",
        ""bankCode"": ""0205"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547567"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547765"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547800"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""547985"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""548232"",
        ""bankCode"": ""0203"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""548237"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""548819"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""548935"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549208"",
        ""bankCode"": ""0059"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549294"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549449"",
        ""bankCode"": ""0010"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549539"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549624"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549839"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549938"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549997"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""549998"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""550074"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""550449"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""550472"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""550473"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""550478"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552095"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552096"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552101"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552143"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552207"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""552608"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552609"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552610"",
        ""bankCode"": ""0206"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552645"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552659"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552679"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""552879"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""553056"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""553058"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""553090"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""553130"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""554297"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554301"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554483"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554548"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554566"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554570"",
        ""bankCode"": ""0099"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554796"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""554960"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""556030"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""556031"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""556033"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""556034"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""556665"",
        ""bankCode"": ""0123"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""557023"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""557113"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""557829"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""557945"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""558443"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558446"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558448"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558460"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558485"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558514"",
        ""bankCode"": ""0134"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558593"",
        ""bankCode"": ""0135"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""558699"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""559056"",
        ""bankCode"": ""0103"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""559096"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""581877"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""588843"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""589004"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""589283"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""589311"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""589318"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""589713"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603072"",
        ""bankCode"": ""0135"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603322"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603343"",
        ""bankCode"": ""0103"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603344"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603650"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""603797"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""606043"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""606329"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""622403"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""627510"",
        ""bankCode"": ""0203"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""627768"",
        ""bankCode"": ""0124"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""638888"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""639001"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""639004"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650052"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650082"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650161"",
        ""bankCode"": ""0067"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650170"",
        ""bankCode"": ""0015"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650173"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650456"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""650987"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""654997"",
        ""bankCode"": ""0124"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""657366"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""657998"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""670606"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""670610"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""670670"",
        ""bankCode"": ""0124"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""670868"",
        ""bankCode"": ""0146"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""671121"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""671155"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676123"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676124"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676166"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676255"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676258"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676283"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676366"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676401"",
        ""bankCode"": ""0123"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676402"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676406"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676460"",
        ""bankCode"": ""0135"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676651"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676827"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""676832"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""677047"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""677193"",
        ""bankCode"": ""0123"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""677238"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""677451"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""685800"",
        ""bankCode"": ""0062"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979202"",
        ""bankCode"": ""0111"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979203"",
        ""bankCode"": ""0111"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979204"",
        ""bankCode"": ""0064"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979206"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979207"",
        ""bankCode"": ""0046"",
        ""cardType"": 1,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""979208"",
        ""bankCode"": ""0046"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979209"",
        ""bankCode"": ""0015"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979210"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979211"",
        ""bankCode"": ""0059"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979212"",
        ""bankCode"": ""0012"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979213"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979215"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": true
    },
    {
        ""binNumber"": ""979216"",
        ""bankCode"": ""0205"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979217"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979218"",
        ""bankCode"": ""0206"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979223"",
        ""bankCode"": ""0032"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979227"",
        ""bankCode"": ""0203"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979233"",
        ""bankCode"": ""0064"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979236"",
        ""bankCode"": ""0062"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979240"",
        ""bankCode"": ""0135"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979241"",
        ""bankCode"": ""0067"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979242"",
        ""bankCode"": ""0099"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979243"",
        ""bankCode"": ""0134"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979244"",
        ""bankCode"": ""0012"",
        ""cardType"": 1,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979246"",
        ""bankCode"": ""0143"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979264"",
        ""bankCode"": ""0032"",
        ""cardType"": 0,
        ""commercialCard"": false
    },
    {
        ""binNumber"": ""979280"",
        ""bankCode"": ""0010"",
        ""cardType"": 0,
        ""commercialCard"": false
    }
]
";

        private static List<CreditCardBinQueryResponse> dataList = null;

        internal static List<CreditCardBinQueryResponse> GetBinList()
        {
            return dataList = dataList ?? Newtonsoft.Json.JsonConvert.DeserializeObject<List<CreditCardBinQueryResponse>>(data);
        }
    }
}
