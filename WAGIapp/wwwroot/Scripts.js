const Graph = ForceGraph();

function inicGraph() {
    console.log("inic start");
    
    const N = 10;
    const gData = {
        nodes: [...Array(N).keys()].map(i => ({ id: i, label: "hewo" })),
        links: [...Array(N).keys()]
            .filter(id => id)
            .map(id => ({
                source: id,
                target: Math.round(Math.random() * (id - 1))
            }))
    };

    
    Graph(document.getElementById('graph'));
    Graph.nodeLabel('text');
    Graph.linkWidth("width");
    Graph.linkLabel('label');
    Graph.nodeColor("color");
    Graph.graphData(gData);

    console.log("inic end");
}

function resizeGraph() {
    Graph.zoomToFit(1000, 100);


    Graph.enableNodeDrag(false);
    Graph.enableZoomInteraction(false);
    Graph.enablePanInteraction(false);
}

function setGraphData(data)
{
    console.log(data);
    Graph.graphData(data);
}

function scrollToEnd(id)
{
    var objDiv = document.getElementById(id);
    objDiv.scrollTop = objDiv.scrollHeight;
}