
let iconList = {
    spin: '<i class="fa fa-spinner fa-spin fa-fw fa-2x"></i>',
    pen: '<i class="fa-solid fa-pen"></i>',
    alert: '<i class="fa-solid fa-triangle-exclamation text-warning fa-beat"></i>',
    ok: '<i class="fa-solid fa-check text-success"></i>',
    switch: '<i class="fa-solid fa-repeat text-warning"></i>'
}

//For loading icon
function loadingTemplate() {
    return iconList.spin;
}

// for edit and delete buttons
function operateFormatter(value, row, index) {
    return [
        '<div class="text-centers w-100 d-flex justify-content-around">',
        '<a class="view"><i class="fa-solid fa-eye btn text-primary"></i></a>',
        '</div>'
    ].join('');
}

// for edit and delete buttons
let selectedviewRow;
window.operateEvents = {
    'click .view': function (e, value, row, index) {
        selectedviewRow = row;
        $("#idDocumentInputModal").val(row.idNotice);
        $("#coteDocumentInputModal").val(row.cote);
        $('#titleDocumentInputModal').val(row.titre);

        $("#exemplaireSearchInput").val('');
        generateSVG();
        $("#viewDocumentModal").modal('show')
    },
    'click .remove': function (e, value, row, index) {
        $("#deleteConfirmationID").text(row.idNotice);
        $("#deleteConfirmation").modal('show')
    }
}

let node ;
//
function generateSVG() {
    $.get($("#chart").data("url"), { cote: selectedviewRow.cote },
        function (data) {
            $("#chart").empty();

            const height = 300;
            const width = 700;

            const boundary = {
                top: 20,
                right: width - 20,
                bottom: height - 20,
                left: 20
            };

            let svg = d3.select("#chart")
                .append("svg")
                .attr("width", width)
                .attr("height", height)

            // Color palette for continents?
            let color = d3.scaleOrdinal()
                .domain([0, 1, 2, 3])
                .range(["#FFA500", 'white', '#75e868', '#ff6112']);

            let Tooltip = d3.select("#chart")
                .append("div")
                .style("opacity", 0)
                .attr("class", "tooltip")
                .style("background-color", "white")
                .style("border", "solid")
                .style("border-width", "2px")
                .style("border-radius", "5px")
                .style("padding", "5px")

            // Three function that change the tooltip when user hover / move / leave a cell
            let mouseover = function (event, d) {
                d3.select(this)
                    .transition()
                    .duration(200)
                    .style("fill-opacity", 1)
                    .attr("r", 25)
                    .style("fill", "rgb(100, 126, 184)"); // Increase the radius to make it bigger
                Tooltip.style("opacity", 1);
            }
            let mousemove = function (event, d) {
                Tooltip
                    .html('<u>' + d.idExemplaire + '</u>' + "<br>" + d.etat)
                    .style("left", (event.x / 2 + 20) + "px")
                    .style("top", (event.y / 2 - 30) + "px")
            }
            let mouseleave = function (event, d) {
                d3.select(this)
                    .transition()
                    .duration(200)
                    .style("fill-opacity", 0.8)
                    .attr("r", 18)
                    .style("fill", "rgb(55, 126, 184)"); // Reset the radius back to its original size
                Tooltip.style("opacity", 0);
            }

            // Initialize the circle: all located at the center of the svg area
            node = svg.append("g")
                .selectAll("circle")
                .data(data)
                .enter()
                .append("circle")
                .attr("class", "node")
                .attr("r", 18)
                .attr("cx", width / 2)
                .attr("cy", height / 2)
                .style("fill", "rgb(55, 126, 184)")
                .style("fill-opacity", 0.8)
                .on("mouseover", mouseover) // What to do when hovered
                .on("mousemove", mousemove)
                .on("mouseleave", mouseleave)
                .call(d3.drag() // call specific function when circle is dragged
                    .on("start", dragstarted)
                    .on("drag", dragged)
                    .on("end", dragended));

            let labels = svg.append("g")
                .selectAll("text")
                .data(data)
                .enter()
                .append("text")
                .attr("x", width / 2)
                .attr("y", height / 2)
                .attr("text-anchor", "middle")
                .attr("dominant-baseline", "central")
                .attr("font-family", "FontAwesome")
                .attr("fill", function (d) { return color(d.idEtat) })
                .attr("font-size", "14px") // Adjust size as needed
                .text(function (d) { return '\uf15b'; }); // Example: Unicode for FontAwesome user icon


            // Features of the forces applied to the nodes:
            var simulation = d3.forceSimulation()
                .force("center", d3.forceCenter().x(width / 2).y(height / 2)) // Attraction to the center of the svg area
                .force('y', d3.forceY().y(height / 2))
                .force("charge", d3.forceManyBody().strength(.8)) // Nodes are attracted one each other of value is > 0
                .force("collide", d3.forceCollide().strength(1).radius(18).iterations(1)) // Force that avoids circle overlapping

            // Apply these forces to the nodes and update their positions.
            // Once the force algorithm is happy with positions ('alpha' value is low enough), simulations will stop.
            simulation
                .nodes(data)
                .on("tick", function (d) {
                    node.attr("cx", function (d) { return d.x = Math.max(boundary.left, Math.min(boundary.right, d.x)); })
                        .attr("cy", function (d) { return d.y = Math.max(boundary.top, Math.min(boundary.bottom, d.y)); });
                    labels.attr("x", function (d) { return d.x = Math.max(boundary.left, Math.min(boundary.right, d.x)); })
                        .attr("y", function (d) { return d.y = Math.max(boundary.top, Math.min(boundary.bottom, d.y)); });
                });

            // What happens when a circle is dragged?
            function dragstarted(event, d) {
                if (!event.active) simulation.alphaTarget(.03).restart();
                d.fx = d.x;
                d.fy = d.y;
            }
            function dragged(event, d) {
                d.fx = event.x;
                d.fy = event.y;
            }
            function dragended(event, d) {
                if (!event.active) simulation.alphaTarget(.03);
                d.fx = null;
                d.fy = null;
            }
        }
    );
}

$("#exemplaireSearchInput").on("input", function () {
    let that = this;
    if($(that).val() == ''){
        node.style("fill" , "rgb(55, 126, 184)");
    }else{
        node.style("fill", function(d) {
            // Check if the idExemplaire matches the user input
            return d.idExemplaire.includes($(that).val()) ? "green" : "rgb(55, 126, 184)";
        });
    }

});