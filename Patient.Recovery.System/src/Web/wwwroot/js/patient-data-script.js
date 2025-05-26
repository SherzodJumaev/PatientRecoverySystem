// API Configuration
const API_BASE_URL = "http://localhost:5262/api";

// Function to fetch and populate patients data
async function loadPatients() {
    try {
        // Show loading state
        showLoading();

        // Fetch data from API
        const response = await fetch(`${API_BASE_URL}/patients`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                // Add any required headers (like Authorization if needed)
                // 'Authorization': 'Bearer ' + token
            },
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const patients = await response.json();

        // Populate the table
        populatePatientsTable(patients);
    } catch (error) {
        console.error("Error loading patients:", error);
        showError("Failed to load patients data");
    } finally {
        hideLoading();
    }
}

// Function to populate the patients table
function populatePatientsTable(patients) {
    const tbody = document.querySelector("#patients-table tbody");

    // Clear existing data
    tbody.innerHTML = "";

    if (!patients || patients.length === 0) {
        tbody.innerHTML =
            '<tr><td colspan="7" class="text-center">No patients found</td></tr>';
        return;
    }

    // Populate table rows
    patients.forEach((patient) => {
        const row = createPatientRow(patient);
        tbody.appendChild(row);
    });
}

// Function to create a table row for a patient
function createPatientRow(patient) {
    const row = document.createElement("tr");

    // Format date of birth
    const dobFormatted = patient.dateOfBirth
        ? new Date(patient.dateOfBirth).toLocaleDateString()
        : "N/A";

    row.innerHTML = `
        <td>${patient.id || "N/A"}</td>
        <td>${patient.firstName || ""} ${patient.lastName || ""}</td>
        <td>${dobFormatted}</td>
        <td>${patient.gender || "N/A"}</td>
        <td>${patient.email || "N/A"}</td>
        <td>${patient.phoneNumber || "N/A"}</td>
        <td>
            <button class="btn btn-sm btn-outline-primary me-1" onclick="viewPatient(${
                patient.id
            })" title="View">
                <i class="fas fa-eye"></i>
            </button>
            <button class="btn btn-sm btn-outline-warning me-1" onclick="editPatient(${
                patient.id
            })" title="Edit">
                <i class="fas fa-edit"></i>
            </button>
            <button class="btn btn-sm btn-outline-danger" onclick="deletePatient(${
                patient.id
            })" title="Delete">
                <i class="fas fa-trash"></i>
            </button>
        </td>
    `;

    return row;
}

// Loading state functions
function showLoading() {
    const tbody = document.querySelector("#patients-table tbody");
    tbody.innerHTML =
        '<tr><td colspan="7" class="text-center"><i class="fas fa-spinner fa-spin"></i> Loading patients...</td></tr>';
}

function hideLoading() {
    // Loading will be hidden when data is populated
}

function showError(message) {
    const tbody = document.querySelector("#patients-table tbody");
    tbody.innerHTML = `<tr><td colspan="7" class="text-center text-danger"><i class="fas fa-exclamation-triangle"></i> ${message}</td></tr>`;
}

// Action functions (implement these based on your needs)
function viewPatient(patientId) {
    console.log("View patient:", patientId);
    // Implement view functionality
    // You can fetch detailed patient data and show in a modal
}

function editPatient(patientId) {
    console.log("Edit patient:", patientId);
    // Implement edit functionality
    // You can populate edit form with patient data
}

async function deletePatient(patientId) {
    if (!confirm("Are you sure you want to delete this patient?")) {
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL}/patients/${patientId}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            },
        });

        if (response.ok) {
            // Reload the patients data
            loadPatients();
            showSuccessMessage("Patient deleted successfully");
        } else {
            throw new Error("Failed to delete patient");
        }
    } catch (error) {
        console.error("Error deleting patient:", error);
        showError("Failed to delete patient");
    }
}

// Utility function to show success messages
function showSuccessMessage(message) {
    // You can implement this with Bootstrap toast or alert
    console.log("Success:", message);
}

// Function to refresh patients data
function refreshPatients() {
    loadPatients();
}

// Initialize - load patients when the patients section is shown
function showPatientsSection() {
    // Hide other sections and show patients section
    document.querySelectorAll(".content-section").forEach((section) => {
        section.style.display = "none";
    });
    document.getElementById("patients-section").style.display = "block";

    // Load patients data
    loadPatients();
}

// Call this when the page loads or when patients section is accessed
document.addEventListener("DOMContentLoaded", function () {
    // If patients section is visible by default, load the data
    const patientsSection = document.getElementById("patients-section");
    if (patientsSection && patientsSection.style.display !== "none") {
        loadPatients();
    }
});

// Optional: Auto-refresh every 30 seconds (uncomment if needed)
// setInterval(loadPatients, 30000);
