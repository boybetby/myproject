var filterdiv1 = `
    <select name="clade2" onchange="" onclick="" id="">
     <option value="">Select clade</option>
    <option value="Tracheophytes">Tracheophytes</option>
    <option value="Angiosperms">Angiosperms</option>
    <option value="Eudicots">Eudicots</option>
    <option value="Monocots">Monocots</option>
    <option value="Strelitziaceae">Strelitziaceae</option>
    <option value="Commelinids">Commelinids</option>
    <option value="Rosids">Rosids</option>
    </select>
`;

function addfilter(){
    var el = document.getElementById('btn-addfilter');
    el.parentNode.removeChild(el);
    $('#secondfilter').append(filterdiv1);
}

