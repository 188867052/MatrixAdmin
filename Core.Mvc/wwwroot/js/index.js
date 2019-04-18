$(document).ready(function ()
{
    new Vue({
        el: "#app",
        data: {
            xml: "",
            referencedXml: "",
            tableInput: "",
            entityInput: "",
            tableColumn: "",
            tableReferencedBy: "",
            referencedEntityXml: "",
            triggers: ""
        },
        methods: {
            onClickBOLMappings()
            {
                getEntityMap(entityInput.value);
            },

            onClickTables()
            {
                getTableInformation(tableInput.value);
            },

            appendEntity()
            {
                get("GetEntityKeyUpData?key=" + entityInput.value).then((response) =>
                {
                    $("#entityList option").remove();
                    $("#entityList").append(response.data);
                });
            },

            appendTables()
            {
                get("GetTableKeyUpData?key=" + tableInput.value).then((response) =>
                {
                    $("#tableList option").remove();
                    $("#tableList").append(response.data);
                });
            }
        }
    });
});

function getEntityMap(name)
{
    window.axios.all([get("GetXmlEntity?entityName=" + name), get("GetReferencedXmlEntity?entityName=" + name)]).then(window.axios.spread(function (xmlData, reference)
    {
        referencedXml.innerHTML = reference.data;
        xml.innerHTML = xmlData.data;
        entityInput.value = name;
        $("#" + window.table._bolMappingsTabId).addClass("active in");
		$("#" + window.table._tableTabId).removeClass("active in");
    }));
}

function getTableInformation(name)
{
    window.axios.all([
        get("GetReferencedBy?tableName=" + name),
        get("GetTableColumnProperties?tableName=" + name),
        get("GetTableReferencedEntity?tableName=" + name),
        get("GetTableTriggers?tableName=" + name)]).
        then(window.axios.spread(
            function (reference, column, referencedEntity, triggersData)
            {
                tableInput.value = name;
                tableColumn.innerHTML = column.data;
                tableReferencedBy.innerHTML = reference.data;
                referencedEntityXml.innerHTML = referencedEntity.data;
                triggers.innerHTML = triggersData.data;
                $("#" + window.table._bolMappingsTabId).removeClass("active in");
				$("#" + window.table._tableTabId).addClass("active in");
                $("table").addClass("table table-striped table-hover table-bordered");
            }));
}

$(".btn-group .btn").click(function ()
{
    if ($(this).hasClass("active"))
    {
        return;
    }
    $(".btn-group .btn").removeClass("active");
    $(this).addClass("active");
});

function get(query)
{
    return window.axios.get(window.table._apiUrl + query);
}