using System.Numerics;
using System.Xml;
using System.Xml.Linq;

XmlDocument doc = new XmlDocument();
doc.Load("users.xml");
var root = doc.DocumentElement;

var nodes = root.SelectNodes("*");

if(nodes != null)
{
    foreach(XmlNode node in nodes)
        Console.WriteLine($"{node.SelectSingleNode("firstname").InnerText.Trim()}");
}

Console.WriteLine();

doc.Load("party.xml");
nodes = doc.SelectNodes(@"//@type");
if (nodes != null)
{
    foreach (XmlNode node in nodes)
        Console.WriteLine($"{node.Name} {node.Value}");
}

void ModifyXmlDoc()
{
    Employee employeeLast = new() { LastName = "Last" };
    Employee employeeAfter = new() { LastName = "After" };
    Employee employeeBefore = new() { LastName = "Before" };

    XmlDocument doc = new XmlDocument();
    doc.Load("users.xml");
    XmlElement root = doc.DocumentElement;

    var firstChild = root.FirstChild;

    var xmlLastEmployee = CreateFullElement(employeeLast);
    root.AppendChild(xmlLastEmployee);

    var xmlAfterEmployee = CreateFullElement(employeeAfter);
    root.InsertAfter(xmlAfterEmployee, firstChild);

    var xmlBeforeEmployee = CreateFullElement(employeeBefore);
    root.InsertBefore(xmlBeforeEmployee, firstChild);

    if (firstChild != null)
        root.RemoveChild(firstChild);

    doc.Save("users.xml");
}
XmlNode CreateFullElement(Employee employee)
{
    XmlElement user = doc.CreateElement("user");
    var firstName = doc.CreateElement("firstname");
    var lastName = doc.CreateElement("lastname");
    var age = doc.CreateElement("age");

    firstName.InnerText = employee.FirstName;
    lastName.InnerText = employee.LastName;
    age.InnerText = employee.Age.ToString();

    user.AppendChild(firstName);
    user.AppendChild(lastName);
    user.AppendChild(age);

    return user;
}
void ReadXmlDocs()
{
    XmlDocument doc = new XmlDocument();
    doc.Load("party.xml");

    XmlElement? root = doc.DocumentElement;
    if (root != null)
        ElementRead(root, 0);



    void ElementRead(XmlNode root, int level)
    {
        if (root != null)
        {
            string tabs = new string('\t', level);

            Console.WriteLine($"{tabs}Element: {root.Name}");
            foreach (XmlAttribute attribute in root.Attributes)
                Console.WriteLine($"{tabs}\tattr: {attribute.Name}: {attribute.Value}");
            Console.WriteLine();

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                    ElementRead(node, level + 1);
                else
                    Console.WriteLine($"{tabs}\tInner Text: {root.InnerText}");
            }


        }
    }
}
void CreateEmployesXml()
{
    List<Employee> employees = new List<Employee>()
{
    new Employee(){ FirstName = "Bob", LastName = "Smith", Age = 23 },
    new Employee(){ FirstName = "Joe", LastName = "Watson", Age = 32 },
};

    XmlDocument xmlDoc = new XmlDocument();
    XmlElement root = xmlDoc.CreateElement("users");
    xmlDoc.AppendChild(root);


    foreach (Employee emp in employees)
    {
        XmlElement user = xmlDoc.CreateElement("user");
        XmlElement firstName = xmlDoc.CreateElement("firstname");
        XmlElement lastName = xmlDoc.CreateElement("lastname");
        XmlElement age = xmlDoc.CreateElement("age");

        firstName.InnerText = emp.FirstName;
        lastName.InnerText = emp.LastName;
        age.InnerText = emp.Age.ToString();

        user.AppendChild(firstName);
        user.AppendChild(lastName);
        user.AppendChild(age);

        root.AppendChild(user);
    }


    xmlDoc.Save("users.xml");
}
class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
