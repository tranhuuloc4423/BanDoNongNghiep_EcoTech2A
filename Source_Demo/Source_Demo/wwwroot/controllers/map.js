// Cấu hình bản đồ
const mapConfig = {
    center: [16.0, 106.0], // Tọa độ trung tâm Việt Nam
    initialZoom: 6, // Mức zoom ban đầu
    zoomControlPosition: 'topright', // Vị trí control zoom
    geoJsonUrls: {
        national: '/map/geoBoundaries-VNM-ADM0_simplified.json',
        province: '/map/geoBoundaries-VNM-ADM1_simplified.json',
        district: '/map/geoBoundaries-VNM-ADM2_simplified.json'
    },
    zoomThresholds: {
        national: 7, // Ngưỡng zoom cho biên giới quốc gia
        province: 10 // Ngưỡng zoom cho biên giới tỉnh
    }
};

// Cấu hình kiểu dáng cho các lớp
const layerStyles = {
    national: {
        color: "#ff0000",
        weight: 3,
        opacity: 1,
        fillColor: "#ffcccc",
        fillOpacity: 0.5
    },
    province: {
        color: "#00ff00",
        weight: 2,
        opacity: 1,
        fillColor: "#ccffcc",
        fillOpacity: 0.5
    },
    district: {
        color: "#0000ff",
        weight: 1,
        opacity: 1,
        fillColor: "#cce5ff",
        fillOpacity: 0.5
    }
};

// Khởi tạo bản đồ
const map = L.map('map', {
    center: mapConfig.center,
    zoom: mapConfig.initialZoom,
    zoomControl: false
});

// Thêm control zoom
L.control.zoom({ position: mapConfig.zoomControlPosition }).addTo(map);

// Thêm lớp bản đồ nền (OSM)
const osmLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

// Thêm lớp bản đồ vệ tinh (OSM Humanitarian)
const osmHumanitarianLayer = L.tileLayer('https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
    attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Tiles style by <a href="https://www.hotosm.org/">Humanitarian OpenStreetMap Team</a>'
});

// Tạo các layer groups
const layers = {
    national: L.layerGroup(),
    province: L.layerGroup(),
    district: L.layerGroup()
};

// Hàm tải GeoJSON
function loadGeoJSON(url, style, layerGroup) {
    fetch(url)
        .then(response => response.json())
        .then(data => {
            L.geoJSON(data, { style }).addTo(layerGroup);
            console.log(`Loaded GeoJSON: ${url}`);
        })
        .catch(error => console.error(`Error loading GeoJSON: ${url}`, error));
}

// Tải dữ liệu GeoJSON cho từng lớp
Object.entries(mapConfig.geoJsonUrls).forEach(([key, url]) => {
    loadGeoJSON(url, layerStyles[key], layers[key]);
});

// Thêm control để chuyển đổi layers nền
const baseLayers = {
    "Bản đồ thường": osmLayer,
    "Bản đồ vệ tinh (HOT)": osmHumanitarianLayer
};
L.control.layers(baseLayers).addTo(map);

// Hàm cập nhật lớp hiển thị dựa trên mức zoom
function updateLayers() {
    const zoomLevel = map.getZoom();

    // Xác định lớp nào nên hiển thị
    let layerToShow;
    if (zoomLevel <= mapConfig.zoomThresholds.national) {
        layerToShow = 'national';
    } else if (zoomLevel <= mapConfig.zoomThresholds.province) {
        layerToShow = 'province';
    } else {
        layerToShow = 'district';
    }

    // Cập nhật trạng thái hiển thị của các lớp
    Object.entries(layers).forEach(([key, layer]) => {
        if (key === layerToShow) {
            if (!map.hasLayer(layer)) {
                layer.addTo(map);
                console.log(`Added layer: ${key}`);
            }
        } else {
            if (map.hasLayer(layer)) {
                map.removeLayer(layer);
                console.log(`Removed layer: ${key}`);
            }
        }
    });
}

// Gọi hàm updateLayers khi khởi tạo
updateLayers();

// Lắng nghe sự kiện zoomend để cập nhật lớp
map.on('zoomend', updateLayers);