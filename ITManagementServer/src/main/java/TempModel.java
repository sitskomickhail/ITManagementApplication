import java.util.ArrayList;
import java.util.List;

public class TempModel {
    private int id;
    private String value;
    private double salary;
    private boolean isValid;
    private List<ArrayTempModel> arrayTempModels;
    private ArrayTempModel arrayTemp;

    public TempModel() {
        arrayTempModels = new ArrayList<>();
    }

    @Override
    public String toString() {
        return "TempModel{" +
                "id=" + id +
                ", value='" + value + '\'' +
                ", salary=" + salary +
                ", isValid=" + isValid +
                ", arrayTempModels=" + arrayTempModels +
                ", arrayTemp=" + arrayTemp +
                '}';
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public double getSalary() {
        return salary;
    }

    public void setSalary(double salary) {
        this.salary = salary;
    }

    public boolean isValid() {
        return isValid;
    }

    public void setValid(boolean valid) {
        isValid = valid;
    }

    public List<ArrayTempModel> getArrayTempModels() {
        return arrayTempModels;
    }

    public void setArrayTempModels(List<ArrayTempModel> arrayTempModels) {
        this.arrayTempModels = arrayTempModels;
    }

    public ArrayTempModel getArrayTemp() {
        return arrayTemp;
    }

    public void setArrayTemp(ArrayTempModel arrayTemp) {
        this.arrayTemp = arrayTemp;
    }
}

class ArrayTempModel {
    private int value1;
    private String value2;
    private boolean value3;

    @Override
    public String toString() {
        return "ArrayTempModel{" +
                "value1=" + value1 +
                ", value2='" + value2 + '\'' +
                ", value3=" + value3 +
                '}';
    }

    public int getValue1() {
        return value1;
    }

    public void setValue1(int value1) {
        this.value1 = value1;
    }

    public String getValue2() {
        return value2;
    }

    public void setValue2(String value2) {
        this.value2 = value2;
    }

    public boolean isValue3() {
        return value3;
    }

    public void setValue3(boolean value3) {
        this.value3 = value3;
    }
}
