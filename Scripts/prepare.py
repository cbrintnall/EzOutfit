import os
import shutil
import xml.etree.ElementTree as ET

prod_package_id = "OtterBee.Ez.Outfit"
prod_mod_name = "EzOutfit"

assembly_name = "EzOutfit.dll"
mod_folder = os.path.dirname(os.path.dirname(__file__))
parent_folder = os.path.dirname(mod_folder)
destination_folder = os.path.join(parent_folder, prod_mod_name)
about_path = os.path.join(destination_folder, "About", "About.xml")

if os.path.exists(destination_folder):
  print(f"Cleaning old mod folder {destination_folder}")
  shutil.rmtree(destination_folder)

print("Copying over mod..")
shutil.copytree(mod_folder, destination_folder)

print("Modifying name in 'About'")
tree = ET.parse(about_path)
root = tree.getroot()
name = root.find("name")
name.text = prod_mod_name
package_id = root.find("packageId")
package_id.text = prod_package_id
tree.write(about_path)

print("Removing clutter..")
shutil.rmtree(os.path.join(destination_folder, "Assets"))
shutil.rmtree(os.path.join(destination_folder, "Scripts"))
shutil.rmtree(os.path.join(destination_folder, "Source"))
shutil.rmtree(os.path.join(destination_folder, ".git"))
os.remove(os.path.join(destination_folder, ".gitignore"))
os.remove(os.path.join(destination_folder, "README.md"))

output_assemblies = os.path.join(destination_folder, "Assemblies")
for item in os.listdir(output_assemblies):
  if item != assembly_name:
    os.remove(os.path.join(output_assemblies, item))

print("Done!")