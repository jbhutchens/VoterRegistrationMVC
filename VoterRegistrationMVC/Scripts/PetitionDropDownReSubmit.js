function refreshPage(value) {
    $(VoterSearchCriteriaModel_PetitionDetailID).empty();
    var url = "/VoterSearch/PetitionChange/";
    $.post(url, { PetitionID: value });
}

$(document).on('change', '#PetitionDDL', function () {
    alert('made it!');
});