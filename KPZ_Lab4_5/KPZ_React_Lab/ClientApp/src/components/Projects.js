import React, { useEffect, useState } from "react";
import "../custom.css";

const URL = "api/projects";

function Projects() {
  const [allProjects, setProjects] = useState([]);

  const getProjects = async () => {
    const options = {
      method: "GET",
    };
    const result = await fetch(URL, options);

    if (result.ok) {
      const projects = await result.json();
      setProjects(projects);
      console.log("All projects:");
      console.log(allProjects);
      return projects;
    }
    return [];
  };

  const addProject = async () => {
    const projectName = document.querySelector("#project-name").value;
    const projectDescription = document.querySelector(
      "#project-description"
    ).value;
    const projectStatus = document.querySelector("#project-status").value;
    const projectMemberCount = document.querySelector("#project-member-count").value;

    const newProject = {
      name: projectName,
      description: projectDescription,
      status: projectStatus,
      memberCount: +projectMemberCount,
    };

    const headers = new Headers();
    headers.set("Content-Type", "application/json");
    const options = {
      method: "POST",
      headers: headers,
      body: JSON.stringify(newProject),
    };

    const result = await fetch(URL, options);
    if (result.ok) {
      const project = await result.json();
      allProjects.push(project);
      setProjects(allProjects.slice());
    }
  };

  const deleteProject = async (id) => {
    const options = {
      method: "DELETE",
      header: new Headers(),
    };
    await fetch(URL + `/${id}`, options);

    setProjects(allProjects.filter((x) => x.id !== id));
  };

  const updateProject = async (oldProject) => {
    const projectName = document.querySelector("#project-name").value;
    const projectDescription = document.querySelector(
      "#project-description"
    ).value;
    const projectStatus= document.querySelector("#project-status").value;
    const projectMemberCount = document.querySelector("#project-member-count").value;

    oldProject.name = projectName;
    oldProject.description = projectDescription;
    oldProject.status = projectStatus;
    oldProject.memberCount = +projectMemberCount;

    const headers = new Headers();
    headers.set("Content-Type", "application/json");
    const options = {
      method: "PATCH",
      headers: headers,
      body: JSON.stringify(oldProject),
    };

    const result = await fetch(URL, options);
    if (result.ok) {
      const project = await result.json();
      const updatedPostIndex = allProjects.findIndex((x) => x.id == project.id);
      allProjects[updatedPostIndex] = project;
      setProjects(allProjects.slice());
    }
  };

  useEffect(() => {
    getProjects();
  }, []);

  return (
    <div>
      <h3 className="text-lg font-semibold mb-8">Creation of projects</h3>
      <div className="flex gap-16">
        <div className="flex flex-col gap-3 w-[400px] mb-8  order-last">
          <div className="flex flex-col gap-2">
            <label for="project-name">Project name</label>
            <input
              className="border-3 border-solid border-green-500  focus:outline-green-800"
              id="project-name"
              type="text"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="project-description">Project description</label>
            <textarea
              className="border-3 border-solid border-green-500 focus:ring-green-800"
              id="project-description"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="project-status gap-2">Project status</label>
            <input
              className="border-3 border-solid border-green-500 focus:outline-green-800"
              id="project-status"
              type="text"
            />
          </div>
          <div className="flex flex-col gap-2">
            <label for="project-member-count">Project member count</label>
            <input
              className="border-3 border-solid border-green-500 focus:outline-green-800"
              id="project-member-count"
              type="number"
            />
          </div>
          <div className="text-center mt-2">
            <button
              className="bg-green-600 px-16 py-3 text-black rounded-md hover:bg-green-800"
              onClick={addProject}
            >
              Add Project
            </button>
          </div>
        </div>
        <div className="h-[500px] overflow-y-auto">
          <table className="w-[800px]">
            <thead>
              <tr>
                <th className="p-2 text-center border-1 border-green-500">Name</th>
                <th className="p-2 text-center border-1 border-green-500">
                  Description
                </th>
                <th className="p-2 text-center border-1 border-green-500">
                  Status
                </th>
                <th className="p-2 text-center border-1 border-green-500">Member count</th>
                <th className="p-2 text-center border-1 border-green-500"></th>
              </tr>
            </thead>
            <tbody>
              {allProjects.map((x) => (
                <ProjectRow
                  key={x.id}
                  project={{
                    name: x.name,
                    description: x.description,
                    status: x.status,
                    memberCount: x.memberCount,
                  }}
                  deleteAction={() => deleteProject(x.id)}
                  updateAction={() => updateProject(x)}
                />
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Projects;

const ProjectRow = ({ project, deleteAction, updateAction }) => {
  return (
    <tr>
      <td className="p-2 text-center border-1 border-green-500"> {project.name}</td>
      <td className="p-2 text-center border-1 border-green-500">
        {project.description}
      </td>
      <td className="p-2 text-center border-1 border-green-500">
        {project.status}
      </td>
      <td className="p-2 text-center border-1 border-green-500">{project.memberCount}</td>
      <td className="p-2 text-center border-1 border-green-500">
        <div className="flex gap-2 justify-center">
          <button
            onClick={deleteAction}
            className="border-green-500 border-2 border-solid rounded-sm hover:bg-slate-400"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke-width="1.5"
              stroke="currentColor"
              class="w-6 h-6"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
              />
            </svg>
          </button>
          <button
            onClick={updateAction}
            className="border-green-500 border-2 border-solid rounded-sm hover:bg-green-600"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="w-6 h-6"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M16.023 9.348h4.992v-.001M2.985 19.644v-4.992m0 0h4.992m-4.993 0l3.181 3.183a8.25 8.25 0 0013.803-3.7M4.031 9.865a8.25 8.25 0 0113.803-3.7l3.181 3.182m0-4.991v4.99"
              />
            </svg>
          </button>
        </div>
      </td>
    </tr>
  );
};
