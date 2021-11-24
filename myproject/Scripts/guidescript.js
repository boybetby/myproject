var filterdiv1 = `
    <select name="family2" onchange="" onclick="return false;" id="">
    <option value="1">Tracheophytes</option>
    <option value="2">Angiosperms</option>
    <option value="3">Eudicots</option>
    <option value="3">Monocots</option>
    <option value="3">Strelitziaceae</option>
    <option value="3">Commelinids</option>
    <option value="3">Rosids</option>
    </select>
`;

function addfilter(){
    var el = document.getElementById('btn-addfilter');
    el.parentNode.removeChild(el);
    $('#secondfilter').append(filterdiv1);
}

