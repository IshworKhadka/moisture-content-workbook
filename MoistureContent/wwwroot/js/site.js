// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function() {

    GetAllValues();

    ParticleSize();

    //Excluded Materials
    $("#checkboxMaterialExcluded").change(function () {
        if (this.checked) {
            $("#textMaterialExcluded").prop("disabled", false);
        }
        else {
            $("#textMaterialExcluded").val("");
            $("#textMaterialExcluded").prop("disabled", true);
        }

    });

    //Validation for Tare ID
    $("#InputTareID").change(function () {
        var text = $("#InputTareID").val();
        if (text.length < 6) {
            alert("Tare ID cannot be more than 5 chars");
            $("#InputTareID").focus();
        }
    });

    //Material Wet Mass is evaluated when data is entered on other fields
    $("#InputTareMaterial").change(function () {
        var tareAndMaterial = $("#InputTareMaterial").val() || 0;
        var tare = $("#InputTareMass").val() || 0;
        var material = tareAndMaterial - tare;
        $("#MaterialWetMass").text(material);
       
    });
    $("#InputTareMass").change(function () {
        var tareAndMaterial = $("#InputTareMaterial").val() || 0;
        var tare = $("#InputTareMass").val() || 0;
        var material = tareAndMaterial - tare;
        $("#MaterialWetMass").text(material);

    });

    $("#txtTotalDryMass").change(function () {
        var totalDryMass = $("#txtTotalDryMass").val() || 0;
        var tare = $("#InputTareMass").val() || 0;
        var material = totalDryMass - tare;
        $("#MatDryMass").text(material);

        //Results and reports are evaluated after entering data on Tare and Material Dry Mass
        CalcuateWaterContent();

    });
    

    $("#btnClearAll").click(function () {
        ClearAll();
    });

    $("#btnClearSecond").click(function () {
        ClearAll();
    });
    

    $('#MethodTypeRadio input').on('change', function () {
        var method = $('input[name=radioMethod]:checked', '#MethodTypeRadio').val();
    });

});

function CalcuateWaterContent() {
    var methodType = $('input[name=radioMethod]:checked', '#MethodTypeRadio').val();
    var dataObject = {
        TareAndMaterialWetMass: $("#InputTareMaterial").val() || 0,
        TareMass: $("#InputTareMass").val() || 0,
        TareAndMaterialDryMass: $("#txtTotalDryMass").val() || 0,
        Method_Type: methodType
    };
    $.ajax({
        type: 'Post',
        url: '/Home/Calculate',
        dataType: 'Json',
        data: dataObject,
        success: function (data) {
            $("#waterContent").text(data + "%");

            //Report Checkboxes 
            if ($("#dryingTemp").val() > 115 || $("#dryingTemp").val() < 105) {
                $("#DryingTemp").prop("checked", true);
            }
            if ($('#checkboxMaterialExcluded').is(':checked')) {
                $("#MatExcluded").prop("checked", true);
            }
        },
        error: function (ex) {
            alert("error" + ex.responseText);
        }
    });
}

//Get values on initialization 
function GetAllValues() {
    $.ajax({
        type: 'Get',
        url: '/Home/GetAllValues',
        dataType: 'Json',
        success: function (data) {
            //All Users (Test, Sampling, Check) and their dates are assumed to be same
            $("#ProjectName").text(data.projectName);
            $("#SampledDate").text(data.sampledDate);
            $("#TestedBy").text(data.testedBy);
            $("#DateTested").text(data.testedDate);
            $("#checkedBy").text(data.checkedBy);
            $("#checkedDate").text(data.dateChecked);
        },
        error: function (ex) {
            alert("error" + ex);
        }
    });
}

function ParticleSize() {
    $.ajax({
        type: 'Get',
        url: '/Home/PartcleSizeDropdownFill',
        dataType: 'Json',
        success: function (data) {
            $("#ParticleSizeDropdown").empty();
            $("#ParticleSizeDropdown").append('<option value="0">N/A</option>');
            $.each(data, function (i, list) {
                $("#ParticleSizeDropdown").append('<option value="' + list+ '">' + list + '</option');
            });
        },
        error: function (ex) {
            alert("error" + ex);
        }
    });
}

//Clears all fields
function ClearAll() {
    GetAllValues();

    $("#ParticleSizeDropdown").val(0);
    $("#dryingTemp").val("110");
    $("#balance").val(0);
    $("#oven").val(0);

    $("#InputTareID").text("");
    $("#InputTareMaterial").val("");
    $("#InputTareMass").val("");
    $("#MaterialWetMass").text("");
    $("#textMaterialExcluded").val("");
    $("#textMaterialExcluded").prop("disabled", true);

    $("#dryBalance").val(0);
    $("#MatDryMass").text("");
    $("#txtTotalDryMass").val("");
    
    $("#waterContent").text("");
    $("#InsuffSampleMass").prop("checked", false);
    $("#DryingTemp").prop("checked", false);
    $("#MatExcluded").prop("checked", false);
}
